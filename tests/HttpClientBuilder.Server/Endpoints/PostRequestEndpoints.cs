namespace HttpClientBuilder.Server.Endpoints;

public static class PostRequestEndpoints
{
    public static WebApplication MapPostEndpoints(this WebApplication app)
    {
        app.MapPost("/api/post/", () => Results.Ok());
        return app;
    }
}