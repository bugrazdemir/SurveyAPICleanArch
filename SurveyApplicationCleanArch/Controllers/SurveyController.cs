using Application.Survey.Commands;
using Application.Survey.Queries;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models.Request;

namespace WebApi.Controllers;

[ApiController]
[Route("surveys")]
public class SurveyController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public SurveyController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] SurveyCreateRequest request, CancellationToken token)
    {
        var options = request.Options.Select(x => new Option
        {
            Description = x.Description,
            Type = x.Type,
            Order = x.Order
        }).ToList();
        var command = new SurveyCreateCommand(request.Question, request.CreatedBy, options, request.Settings);
        await _mediator.Send(command, token);
        return Ok();
    }
}
