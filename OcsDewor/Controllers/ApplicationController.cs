using Microsoft.AspNetCore.Mvc;
using OcsDewor.Controllers.Dto;
using OcsDewor.Services;
using OcsDewor.Controllers.Dto;
using OcsDewor.Models;

namespace OcsDewor.Controllers;

[ApiController]
[Route("api")]
public class ApplicationsController(IApplicationService _service) : ControllerBase
{   
    
    [HttpPost("applications")]
    public async Task<ActionResult<ApplicationResponseDto>> CreateApplication([FromBody]CreateApplicationRequestDto applicationDto)
    {
        var result = await _service.CreateApplication(applicationDto);
        if (result == null) return BadRequest();
        return result;
    }
    
    [HttpPut("applications/{applicationId:guid}")]
    public async Task<ActionResult<ApplicationResponseDto>> UpdateApplication([FromBody]UpdateApplicationRequestDto applicationDto, Guid applicationId)
    {
        var result = await _service.UpdateApplication(applicationDto, applicationId);
        if (result == new ApplicationResponseDto())
        {
            return BadRequest();
        }
        return result;
    }
    
    [HttpDelete("applications/{applicationId:guid}")]
    public async Task<ActionResult> DeleteApplication(Guid applicationId)
    {
        var result = await _service.DeleteApplication(applicationId);
        if (!result)
        {
            return BadRequest();
        }
        return Ok();
    }
    
    [HttpPost("applications/{applicationId:guid}/submit")]
    public async Task<ActionResult> SubmitApplication(Guid applicationId)
    {
        var result = await _service.SubmitApplication(applicationId);
        if (!result)
        {
            return BadRequest();
        }
        
        return Ok();
    }
    
    [HttpGet("applications")]
    public async Task<ActionResult<IEnumerable<ApplicationResponseDto>>> GetApplication([FromQuery]string submittedAfter = "", [FromQuery]string unSubmittedOlder = "")
    {
        if (submittedAfter == "" && unSubmittedOlder == "")
        {
            return BadRequest();
        }
        if (unSubmittedOlder == "")
        {
            var result = await _service.GetApplicationSubmittedAfter(submittedAfter);
            var applicationResponseDtos = result.ToList();
            return applicationResponseDtos.Count == 0 ? BadRequest() : new ActionResult<IEnumerable<ApplicationResponseDto>>(applicationResponseDtos.ToList());
        }
        if (submittedAfter == "")
        {
            var result = await _service.GetApplicationUnSubmittedOlder(unSubmittedOlder);
            var applicationResponseDtos = result.ToList();
            return applicationResponseDtos.Count == 0 ? BadRequest() : new ActionResult<IEnumerable<ApplicationResponseDto>>(applicationResponseDtos.ToList());
        }
        
        return BadRequest();
    }
    
    [HttpGet("users/{userId:guid}/currentapplication")]
    public async Task<ActionResult<ApplicationResponseDto>> GetCurrentApplicationByUserId(Guid userId)
    {
        var result = await _service.GetCurrentApplicationByUserId(userId);
        
        return Ok();
    }
    
    [HttpGet("applications/{applicationId:guid}")]
    public async Task<ActionResult<ApplicationResponseDto>> GetApplicationById(Guid applicationId)
    {
        var result = await _service.GetApplicationById(applicationId);

        return result;
    }
    
    [HttpGet("activities")]
    public async Task<ActionResult<IEnumerable<ActivityDto>>> GetAllActivities()
    {
        var result = await _service.GetAllActivities();
        var activityDtos = result.ToList();
        return activityDtos.Count == 0 ? BadRequest() : new ActionResult<IEnumerable<ActivityDto>>(activityDtos.ToList());
    }
}