using AmmFramework.Core.Contracts.ApplicationServices.Commands;
using AmmFramework.Core.Contracts.ApplicationServices.Events;
using AmmFramework.Core.Contracts.ApplicationServices.Queries;
using AmmFramework.Utilities;
using Microsoft.AspNetCore.Http;

namespace AmmFramework.EndPoints.Web.Extensions;

public static class HttpContextExtensions
{
    public static ICommandDispatcher CommandDispatcher(this HttpContext httpContext) =>
        (ICommandDispatcher)httpContext.RequestServices.GetService(typeof(ICommandDispatcher));

    public static IQueryDispatcher QueryDispatcher(this HttpContext httpContext) =>
        (IQueryDispatcher)httpContext.RequestServices.GetService(typeof(IQueryDispatcher));
    public static IEventDispatcher EventDispatcher(this HttpContext httpContext) =>
        (IEventDispatcher)httpContext.RequestServices.GetService(typeof(IEventDispatcher));
    public static FrameworkServices FrameworkApplicationContext(this HttpContext httpContext) =>
        (FrameworkServices)httpContext.RequestServices.GetService(typeof(FrameworkServices));
}