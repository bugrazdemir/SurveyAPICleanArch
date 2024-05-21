using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models.Request;

namespace WebApi.Controllers;

[ApiController]
[Route("votes")]
public class VotesController : ControllerBase
{
    private readonly IMediator _mediator;

    public VotesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("{surveyId}")]
    public async Task<IActionResult> UseVote([FromRoute] int surveyId, VoteCreateRequest request, CancellationToken cancellationToken)
    {
        await _mediator.Send(request.ToCommand(surveyId), cancellationToken);
        return Created();
    }
}