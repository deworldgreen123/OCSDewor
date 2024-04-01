using System.ComponentModel.DataAnnotations;

namespace OcsDewor.Models;

public class Application
{
    public Guid Id { get; set; }
    public Guid AuthorId { get; set; }
    public int? TypeActivityId { get; set; }
    
    [StringLength(100)]
    public string? Name { get; set; }
    
    [StringLength(300)]
    public string? Description { get; set; }
    
    [StringLength(1000)]
    public string? PlanApplication { get; set; }
    public bool IsUnSubmitted { get; set; }
    public DateTime? LastUpdate { get; set; }
}