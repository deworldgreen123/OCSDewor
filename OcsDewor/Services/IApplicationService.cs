using OcsDewor.Controllers.Dto;
using OcsDewor.Models;

namespace OcsDewor.Services;

public interface IApplicationService
{
    Task<ApplicationResponseDto> CreateApplication(CreateApplicationRequestDto application);
    Task<ApplicationResponseDto> UpdateApplication(UpdateApplicationRequestDto application, Guid applicationId);
    Task<bool> DeleteApplication(Guid applicationId);
    Task<bool> SubmitApplication(Guid applicationId);
    Task<IEnumerable<ApplicationResponseDto>> GetApplicationSubmittedAfter(string submittedAfter);
    Task<IEnumerable<ApplicationResponseDto>> GetApplicationUnSubmittedOlder(string unSubmittedOlder);
    Task<ApplicationResponseDto> GetCurrentApplicationByUserId(Guid userId);
    Task<ApplicationResponseDto> GetApplicationById(Guid applicationId);
    Task<IEnumerable<ActivityDto>> GetAllActivities();
}