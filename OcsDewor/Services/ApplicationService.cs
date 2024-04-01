using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using OcsDewor.Controllers.Dto;
using OcsDewor.Models;
using OcsDewor.Repositories;

namespace OcsDewor.Services;

public class ApplicationService : IApplicationService
{
    private readonly IApplicationRepository _applicationRepository;
    private readonly ITypeActivityRepository _activityRepository;
    private readonly string _patterDate = "yyyy-MM-dd H:mm:ss.ff";
    
    public ApplicationService(IApplicationRepository studentRepository, ITypeActivityRepository activityRepository)
    {
        _applicationRepository = studentRepository;
        _activityRepository = activityRepository;
    }
    public async Task<ApplicationResponseDto> CreateApplication(CreateApplicationRequestDto application)
    {
        if (application.author.Equals(null))
        {
            return null!;
        }

        if (await _applicationRepository.CheckSubmittedApplication(application.author))
            return null!;
        
        var unionParameters = "";
        var app = new Application()
        {
            AuthorId = application.author
        };

        unionParameters += application.activity; 
        unionParameters += application.name;
        unionParameters += application.description; 
        unionParameters += application.outline;
        
        if (unionParameters == "") return null!;
        
        app.TypeActivityId = await _activityRepository.GetActivityByName(application.activity);
        if (app.TypeActivityId == 0) return null!;
        
        app.Name = application.name;
        app.Description = application.description;
        app.PlanApplication = application.outline;
        
        app.IsUnSubmitted = true;
        app.LastUpdate = DateTime.UtcNow;

        
        var res = await _applicationRepository.InsertApplication(app);
        return new ApplicationResponseDto()
        {
            id = res.Id,
            author = res.AuthorId,
            activity = await _activityRepository.GetActivityById(res.TypeActivityId),
            name = res.Name,
            description = res.Description,
            outline = res.PlanApplication,
        };

    }

    public async Task<ApplicationResponseDto> UpdateApplication(UpdateApplicationRequestDto application, Guid applicationId)
    {
        var curApplication = await _applicationRepository.GetApplicationById(applicationId);
        if (!curApplication.IsUnSubmitted) return null!;
        
        var unionParameters = "";
        unionParameters += application.activity; 
        unionParameters += application.name;
        unionParameters += application.description; 
        unionParameters += application.outline;
        
        if (unionParameters == "") return null!;

        curApplication.TypeActivityId = await _activityRepository.GetActivityByName(application.activity);
        if (curApplication.TypeActivityId == 0) return null!;
        
        curApplication.Name = application.name;
        curApplication.Description = application.description;
        curApplication.PlanApplication = application.outline;
        
        var res = await _applicationRepository.UpdateApplication(curApplication);

        return new ApplicationResponseDto()
        {
            id = res.Id,
            author = res.AuthorId,
            activity = (await _activityRepository.GetActivityById(res.TypeActivityId)),
            name = res.Name,
            description = res.Description,
            outline = res.PlanApplication,
        };
    }

    public async Task<bool> DeleteApplication(Guid applicationId)
    {
        var curApplication = await _applicationRepository.GetApplicationById(applicationId);
        
        if (curApplication == new Application()) return false;
        
        if (!curApplication.IsUnSubmitted) return false;

        await _applicationRepository.DeleteApplication(applicationId);
        return true;
    }

    public async Task<bool> SubmitApplication(Guid applicationId)
    {
        var curApplication = await _applicationRepository.GetApplicationById(applicationId);
        
        if (curApplication == new Application()) return false;
        
        if (!curApplication.IsUnSubmitted) return false;

        curApplication.IsUnSubmitted = false;
        await _applicationRepository.UpdateApplication(curApplication);
        return true;
    }

    public async Task<IEnumerable<ApplicationResponseDto>> GetApplicationSubmittedAfter(string submittedAfter)
    {
        if (!DateTime.TryParse(submittedAfter, out var submittedAfterDate)) return null!;
        
        var res = (await _applicationRepository.GetApplications()).Where(a => !a.IsUnSubmitted && a.LastUpdate > submittedAfterDate);
        var applications = new List<ApplicationResponseDto>();
        foreach (var application in res)
        {
            applications.Add(new ApplicationResponseDto()
            {
                id = application.Id,
                author = application.AuthorId,
                activity = (await _activityRepository.GetActivityById(application.TypeActivityId)),
                name = application.Name,
                description = application.Description,
                outline = application.PlanApplication,
            });
        }
        return applications;
    }

    public async Task<IEnumerable<ApplicationResponseDto>> GetApplicationUnSubmittedOlder(string unSubmittedOlder)
    {
        if (!DateTime.TryParse(unSubmittedOlder, out var unSubmittedOlderDate)) return null!;
        
        var res = (await _applicationRepository.GetApplications()).Where(a => a.IsUnSubmitted && a.LastUpdate < unSubmittedOlderDate);
        var applications = new List<ApplicationResponseDto>();
        foreach (var application in res)
        {
            applications.Add(new ApplicationResponseDto()
            {
                id = application.Id,
                author = application.AuthorId,
                activity = (await _activityRepository.GetActivityById(application.TypeActivityId)),
                name = application.Name,
                description = application.Description,
                outline = application.PlanApplication,
            });
        }
        return applications;
    }

    public async Task<ApplicationResponseDto> GetCurrentApplicationByUserId(Guid userId)
    {
        if (!await _applicationRepository.CheckSubmittedApplication(userId))
        {
            return null!;
        }

        var res = (await _applicationRepository.GetApplications()).FirstOrDefault(a => a.IsUnSubmitted && a.AuthorId == userId);

        return new ApplicationResponseDto()
        {
            id = res.Id,
            author = res.AuthorId,
            activity = (await _activityRepository.GetActivityById(res.TypeActivityId)),
            name = res.Name,
            description = res.Description,
            outline = res.PlanApplication,
        };
    }

    public async Task<ApplicationResponseDto> GetApplicationById(Guid applicationId)
    {
        var res = await _applicationRepository.GetApplicationById(applicationId);
        if (res == new Application()) return null!;
        return new ApplicationResponseDto()
        {
            id = res.Id,
            author = res.AuthorId,
            activity = await _activityRepository.GetActivityById(res.TypeActivityId),
            name = res.Name,
            description = res.Description,
            outline = res.PlanApplication,
        };
    }

    public async Task<IEnumerable<ActivityDto>> GetAllActivities()
    {
        var res = await _activityRepository.GetActivities();
        var activities = res.Select(activity => new ActivityDto() { activity = activity.Name, description = activity.Description });

        return activities;
    }
}