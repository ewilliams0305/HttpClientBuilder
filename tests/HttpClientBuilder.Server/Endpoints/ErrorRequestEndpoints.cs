using Microsoft.AspNetCore.Mvc;

namespace HttpClientBuilder.Server.Endpoints;

public static class ErrorRequestEndpoints
{
    public static WebApplication MapErrorEndpoints(this WebApplication app)
    {
        app.MapGet("/api/errors/400", () => Results.BadRequest());
        app.MapPost("/api/errors/400", () => Results.BadRequest());
        app.MapPut("/api/errors/400", () => Results.BadRequest());
        app.MapDelete("/api/errors/400", () => Results.BadRequest());

        app.MapGet("/api/errors/404", () => Results.NotFound());
        app.MapPost("/api/errors/404", () => Results.NotFound());
        app.MapPut("/api/errors/404", () => Results.NotFound());
        app.MapDelete("/api/errors/404", () => Results.NotFound());

        app.MapGet("/api/errors/500", () => Results.StatusCode(500));
        app.MapPost("/api/errors/500", () => Results.StatusCode(500));
        app.MapPut("/api/errors/500", () => Results.StatusCode(500));
        app.MapDelete("/api/errors/500", () => Results.StatusCode(500));

        return app;
    }
    
    
}