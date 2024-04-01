using Microsoft.EntityFrameworkCore;
using OcsDewor.Models;

namespace OcsDewor.Repositories;

public class ApplicationRepository(ApplicationContext context) : IApplicationRepository, IDisposable
{
    public async Task<IEnumerable<Application>> GetApplications()
    {
        return await context.Applications.ToListAsync();
    }

    public async Task<Application> GetApplicationById(Guid id)
    {
        var application = await context.Applications.FirstOrDefaultAsync(a => a.Id == id);
        return application ?? new Application();
    }

    public async Task<Application> InsertApplication(Application application)
    {
        var result = await context.Applications.AddAsync(application);
        await Save();
        return result.Entity;
    }

    public async Task DeleteApplication(Guid id)
    {
        var application = await context.Applications.FirstOrDefaultAsync(a => a.Id == id);
        context.Applications.Remove(application);
        await Save();
    }

    public async Task<Application> UpdateApplication(Application application)
    {
        var result = await context.Applications.FirstOrDefaultAsync(a => a.Id == application.Id);
        if (result != null)
        {
            result.TypeActivityId = application.TypeActivityId;
            result.Name = application.Name;
            result.Description = application.Description;
            result.PlanApplication = application.PlanApplication;
            result.IsUnSubmitted = application.IsUnSubmitted;
            result.LastUpdate = DateTime.UtcNow;
            
            await context.SaveChangesAsync();

            return result;
        }

        return null!;
    }

    public async Task<bool> CheckSubmittedApplication(Guid authorId)
    {
        var res = await context.Applications.ToListAsync();
        return res.Any(a => a.IsUnSubmitted && a.AuthorId == authorId);
    }

    public async Task Save()
    {
        await context.SaveChangesAsync();
    }
    
    private bool _disposed;

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                context.Dispose();
            }
        }
        _disposed = true;
    }
    
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}