using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using PetStock.API.Catalog.Application.Services;
using PetStock.API.Catalog.Domain.Model.Queries;
using PetStock.API.Catalog.Interfaces.REST.Resources;
using PetStock.API.Catalog.Interfaces.REST.Transform;
using PetStock.API.Resources;

namespace PetStock.API.Catalog.Interfaces.REST;

/// <summary>Endpoints for product categories.</summary>
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Categories")]
public class CategoriesController(
    ICategoryCommandService categoryCommandService,
    ICategoryQueryService categoryQueryService,
    IStringLocalizer<SharedResource> localizer)
    : ControllerBase
{
    /// <summary>Creates a category.</summary>
    [HttpPost]
    [ProducesResponseType(typeof(CategoryResource), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> CreateCategory([FromBody] CreateCategoryResource resource,
        CancellationToken cancellationToken)
    {
        var createCategoryCommand = CreateCategoryCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await categoryCommandService.Handle(createCategoryCommand, cancellationToken);
        return ActionResultFromCreateCategoryResultAssembler.ToActionResultFromCreateCategoryResult(
            result,
            this,
            localizer,
            nameof(GetCategoryById));
    }

    /// <summary>Lists all categories.</summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CategoryResource>), StatusCodes.Status200OK)]
    public async Task<ActionResult> GetAllCategories(CancellationToken cancellationToken = default)
    {
        var getAllCategoriesQuery = new GetAllCategoriesQuery();
        var categories = await categoryQueryService.Handle(getAllCategoriesQuery, cancellationToken);
        var resources = categories.Select(CategoryResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    /// <summary>Gets one category by id.</summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(CategoryResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetCategoryById(int id, CancellationToken cancellationToken = default)
    {
        var getCategoryByIdQuery = new GetCategoryByIdQuery(id);
        var category = await categoryQueryService.Handle(getCategoryByIdQuery, cancellationToken);
        if (category is null) return NotFound();
        var resource = CategoryResourceFromEntityAssembler.ToResourceFromEntity(category);
        return Ok(resource);
    }
}
