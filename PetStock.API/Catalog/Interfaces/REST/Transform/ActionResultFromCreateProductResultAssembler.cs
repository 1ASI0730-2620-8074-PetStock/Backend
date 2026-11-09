using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using PetStock.API.Catalog.Application.Errors;
using PetStock.API.Catalog.Domain.Model.Aggregates;
using PetStock.API.Resources;
using PetStock.API.Shared.Application.Patterns;

namespace PetStock.API.Catalog.Interfaces.REST.Transform;

/// <summary>Turns the create result into 201, 400 or 500 with a translated message.</summary>
public static class ActionResultFromCreateProductResultAssembler
{
    public static ActionResult ToActionResultFromCreateProductResult(
        Result<Product, CreateProductError> result,
        ControllerBase controller,
        IStringLocalizer<SharedResource> localizer,
        string getProductByIdActionName) =>
        result switch
        {
            Result<Product, CreateProductError>.Success success =>
                controller.CreatedAtAction(getProductByIdActionName, new { id = success.Value.Id },
                    ProductResourceFromEntityAssembler.ToResourceFromEntity(success.Value)),
            Result<Product, CreateProductError>.Failure failure => failure.Error switch
            {
                CreateProductError.CategoryNotFound =>
                    controller.BadRequest(localizer["ProductCategoryNotFound"].Value),

                CreateProductError.SupplierNotFound =>
                    controller.BadRequest(localizer["ProductSupplierNotFound"].Value),

                CreateProductError.InvalidProductData =>
                    controller.BadRequest(localizer["InvalidProductRequest"].Value),

                CreateProductError.UnexpectedError =>
                    controller.Problem(
                        title: localizer["UnexpectedServerError"].Value,
                        detail: localizer["UnexpectedErrorCreatingProduct"].Value,
                        statusCode: 500),

                _ => controller.Problem(
                    title: localizer["UnexpectedServerError"].Value,
                    detail: localizer["UnexpectedErrorProcessingRequest"].Value,
                    statusCode: 500)
            },

            _ => controller.Problem(
                title: localizer["UnexpectedServerError"].Value,
                detail: localizer["UnexpectedErrorProcessingRequest"].Value,
                statusCode: 500)
        };
}