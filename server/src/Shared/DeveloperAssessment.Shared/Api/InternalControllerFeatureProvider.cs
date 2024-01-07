using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using System.Reflection;

namespace DeveloperAssessment.Shared.Api;

/// <summary>
/// As part of creating a modular monolith, this provider enables us to expose controllers marked as internal.
/// </summary>
internal sealed class InternalControllerFeatureProvider : ControllerFeatureProvider
{
    /// <inheritdoc />
    protected override bool IsController(TypeInfo typeInfo)
    {
        if (!typeInfo.IsClass)
        {
            return false;
        }

        if (typeInfo.IsAbstract)
        {
            return false;
        }

        if (typeInfo.ContainsGenericParameters)
        {
            return false;
        }

        if (typeInfo.IsDefined(typeof(NonControllerAttribute)))
        {
            return false;
        }

        return typeInfo.Name.EndsWith("Controller", StringComparison.OrdinalIgnoreCase) ||
               typeInfo.IsDefined(typeof(ControllerAttribute));
    }
}
