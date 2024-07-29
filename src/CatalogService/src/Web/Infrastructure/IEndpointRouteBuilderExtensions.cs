using System.Diagnostics.CodeAnalysis;

namespace CatalogService.Web.Infrastructure;

public static class IEndpointRouteBuilderExtensions
{
    public static RouteGroupBuilder MapGet(this RouteGroupBuilder builder, Delegate handler, [StringSyntax("Route")] string pattern = "")
    {
        Guard.Against.AnonymousMethod(handler);

        builder.MapGet(pattern, handler)
            .WithName(handler.Method.Name);

        return builder;
    }

    public static RouteGroupBuilder MapPost(this RouteGroupBuilder builder, Delegate handler, [StringSyntax("Route")] string pattern = "")
    {
        Guard.Against.AnonymousMethod(handler);

        builder.MapPost(pattern, handler)
            
            .WithName(handler.Method.Name);

        return builder;
    }

    public static RouteGroupBuilder MapPut(this RouteGroupBuilder builder, Delegate handler, [StringSyntax("Route")] string pattern)
    {
        Guard.Against.AnonymousMethod(handler);

        builder.MapPut(pattern, handler)
            .WithName(handler.Method.Name);

        return builder;
    }

    public static RouteGroupBuilder MapDelete(this RouteGroupBuilder builder, Delegate handler, [StringSyntax("Route")] string pattern)
    {
        Guard.Against.AnonymousMethod(handler);

        builder.MapDelete(pattern, handler)
            .WithName(handler.Method.Name);

        return builder;
    }
}
