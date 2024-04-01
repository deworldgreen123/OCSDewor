using OcsDewor.Models;

namespace OcsDewor.Repositories;

public interface ITypeActivityRepository
{
    Task<IEnumerable<TypeActivity>> GetActivities();
    Task<string> GetActivityById(int? id);
    Task<int> GetActivityByName(string name);
    Task<TypeActivity> InsertActivity(TypeActivity activity);
    Task DeleteActivity(int id);
}