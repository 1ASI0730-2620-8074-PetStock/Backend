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
[Tags("Users")]
public class UsersController(
    IUserCommandService userCommandService,
    IUserQueryService userQueryService,
    IStringLocalizer<SharedResource> localizer)
    : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(UserResource), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> CreateUser([FromBody] CreateUserResource resource, CancellationToken cancellationToken)
    {
        var createUserCommand = CreateUserCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await userCommandService.Handle(createUserCommand, cancellationToken);
        return ActionResultFromCreateUserResultAssembler.ToActionResultFromCreateUserResult(result, this, localizer, nameof(GetUserById));
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<UserResource>), StatusCodes.Status200OK)]
    public async Task<ActionResult> GetAllUsers(CancellationToken cancellationToken = default)
    {
        var getAllUsersQuery = new GetAllUsersQuery();
        var users = await userQueryService.Handle(getAllUsersQuery, cancellationToken);
        var resources = users.Select(UserResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(UserResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetUserById(int id, CancellationToken cancellationToken = default)
    {
        var getUserByIdQuery = new GetUserByIdQuery(id);
        var user = await userQueryService.Handle(getUserByIdQuery, cancellationToken);
        if (user is null) return NotFound();
        var resource = UserResourceFromEntityAssembler.ToResourceFromEntity(user);
        return Ok(resource);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(UserResource), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> UpdateUser(int id, [FromBody] UpdateUserResource resource, CancellationToken cancellationToken)
    {
        var updateUserCommand = UpdateUserCommandFromResourceAssembler.ToCommandFromResource(id, resource);
        var result = await userCommandService.Handle(updateUserCommand, cancellationToken);
        return ActionResultFromUpdateUserResultAssembler.ToActionResultFromUpdateUserResult(result, this, localizer);
    }
}