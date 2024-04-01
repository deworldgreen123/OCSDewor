using Microsoft.EntityFrameworkCore;
using OcsDewor.Models;

namespace OcsDewor.Repositories;

public sealed class TypeActivityRepository(ApplicationContext context): ITypeActivityRepository, IDisposable
{
    public async Task<IEnumerable<TypeActivity>> GetActivities()
    {
        return await context.TypeActivities.ToListAsync();
    }

    public async Task<string> GetActivityById(int? id)
    {
        var activity = await context.TypeActivities.FirstOrDefaultAsync(a => a.Id == id);
        return activity?.Name ?? "";
    }

    public async Task<int> GetActivityByName(string name)
    {
        var activity = await context.TypeActivities.FirstOrDefaultAsync(a => a.Name == name);
        return activity?.Id ?? 0;
    }

    public async Task<TypeActivity> InsertActivity(TypeActivity activity)
    {
        var result = await context.TypeActivities.AddAsync(activity);
        await Save();
        return result.Entity;
    }

    public async Task DeleteActivity(int id)
    {
        var activity = await context.TypeActivities.FirstOrDefaultAsync(a => a.Id == id);
        if (activity != null) context.TypeActivities.Remove(activity);
    }

    public async Task Save()
    {
        await context.SaveChangesAsync();
    }
    
    private bool _disposed;

    private void Dispose(bool disposing)
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