using Microsoft.AspNetCore.Mvc.ApplicationModels;
using PetStock.API.Shared.Infrastructure.Interfaces.ASP.Configuration.Extensions;

namespace PetStock.API.Shared.Infrastructure.Interfaces.ASP.Configuration;

/// <summary>Replaces [controller] in routes with its kebab-case name.</summary>
public class KebabCaseRouteNamingConvention : IControllerModelConvention
{
    /// <summary>Applies the kebab-case name to the controller and its actions.</summary>
    public void Apply(ControllerModel controller)
    {
        foreach (var selector in controller.Selectors)
            selector.AttributeRouteModel = ReplaceControllerTemplate(selector, controller.ControllerName);

        foreach (var selector in controller.Actions.SelectMany(a => a.Selectors))
            selector.AttributeRouteModel = ReplaceControllerTemplate(selector, controller.ControllerName);
    }

    private static AttributeRouteModel? ReplaceControllerTemplate(SelectorModel selector, string name)
    {
        return selector.AttributeRouteModel != null
            ? new AttributeRouteModel
            {
                Template = selector.AttributeRouteModel.Template?.Replace("[controller]", name.ToKebabCase())
            }
            : null;
    }
}