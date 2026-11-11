using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using PetStock.API.Profiles.Application.Services;
using PetStock.API.Profiles.Domain.Model.Queries;
using PetStock.API.Profiles.Interfaces.REST.Resources;
using PetStock.API.Profiles.Interfaces.REST.Transform;
using PetStock.API.Resources;

namespace PetStock.API.Profiles.Interfaces.REST;

/// <summary>Endpoints for user preferences.</summary>
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Preferences")]
public class PreferencesController(
    IUserPreferenceCommandService userPreferenceCommandService,
    IUserPreferenceQueryService userPreferenceQueryService,
    IStringLocalizer<SharedResource> localizer)
    : ControllerBase
{
    /// <summary>Creates the preferences of a user. A user can only have one record.</summary>
    [HttpPost]
    [ProducesResponseType(typeof(UserPreferenceResource), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> CreateUserPreference([FromBody] CreateUserPreferenceResource resource,
        CancellationToken cancellationToken)
    {
        var createUserPreferenceCommand =
            CreateUserPreferenceCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await userPreferenceCommandService.Handle(createUserPreferenceCommand, cancellationToken);
        return ActionResultFromCreateUserPreferenceResultAssembler.ToActionResultFromCreateUserPreferenceResult(
            result,
            this,
            localizer,
            nameof(GetUserPreferenceById));
    }

    /// <summary>Lists all user preferences.</summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<UserPreferenceResource>), StatusCodes.Status200OK)]
    public async Task<ActionResult> GetAllUserPreferences(CancellationToken cancellationToken = default)
    {
        var getAllUserPreferencesQuery = new GetAllUserPreferencesQuery();
        var userPreferences = await userPreferenceQueryService.Handle(getAllUserPreferencesQuery, cancellationToken);
        var resources = userPreferences.Select(UserPreferenceResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    /// <summary>Gets one user preference by id.</summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(UserPreferenceResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetUserPreferenceById(int id, CancellationToken cancellationToken = default)
    {
        var getUserPreferenceByIdQuery = new GetUserPreferenceByIdQuery(id);
        var userPreference = await userPreferenceQueryService.Handle(getUserPreferenceByIdQuery, cancellationToken);
        if (userPreference is null) return NotFound();
        var resource = UserPreferenceResourceFromEntityAssembler.ToResourceFromEntity(userPreference);
        return Ok(resource);
    }

    /// <summary>Updates the preferences of a user.</summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(UserPreferenceResource), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> UpdateUserPreference(int id, [FromBody] UpdateUserPreferenceResource resource,
        CancellationToken cancellationToken)
    {
        var updateUserPreferenceCommand =
            UpdateUserPreferenceCommandFromResourceAssembler.ToCommandFromResource(id, resource);
        var result = await userPreferenceCommandService.Handle(updateUserPreferenceCommand, cancellationToken);
        return ActionResultFromUpdateUserPreferenceResultAssembler.ToActionResultFromUpdateUserPreferenceResult(
            result,
            this,
            localizer);
    }
}
