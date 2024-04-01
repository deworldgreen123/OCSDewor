using OcsDewor.Models;

namespace OcsDewor.Repositories;

public interface IApplicationRepository
{
    Task<IEnumerable<Application>> GetApplications();
    Task<Application> GetApplicationById(Guid id);
    Task<Application> InsertApplication(Application application);
    Task DeleteApplication(Guid id);
    Task<Application> UpdateApplication(Application application);
    Task<bool> CheckSubmittedApplication(Guid id);
}