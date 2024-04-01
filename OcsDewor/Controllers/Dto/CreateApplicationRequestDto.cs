namespace OcsDewor.Controllers.Dto;

public class CreateApplicationRequestDto
{
    public Guid author { get; set; }
    public string activity { get; set; }
    public string name { get; set; }
    public string description { get; set; }
    public string outline { get; set; }
}