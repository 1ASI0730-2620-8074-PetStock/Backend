using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using PetStock.API.IAM.Application.Services;
using PetStock.API.IAM.Domain.Model.Queries;
using PetStock.API.IAM.Interfaces.REST.Resources;
using PetStock.API.IAM.Interfaces.REST.Transform;
using PetStock.API.Resources;

namespace PetStock.API.IAM.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Sessions")]
public class SessionsController(
    ISessionCommandService sessionCommandService,
    ISessionQueryService sessionQueryService,
    IStringLocalizer<SharedResource> localizer)
    : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(SessionResource), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> SignIn([FromBody] SignInResource resource, CancellationToken cancellationToken)
    {
        var signInCommand = SignInCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await sessionCommandService.Handle(signInCommand, cancellationToken);
        return ActionResultFromSignInResultAssembler.ToActionResultFromSignInResult(result, this, localizer, nameof(GetSessionById));
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(SessionResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetSessionById(int id, CancellationToken cancellationToken = default)
    {
        var getSessionByIdQuery = new GetSessionByIdQuery(id);
        var session = await sessionQueryService.Handle(getSessionByIdQuery, cancellationToken);
        if (session is null) return NotFound();
        var resource = SessionResourceFromEntityAssembler.ToResourceFromEntity(session);
        return Ok(resource);
    }
}