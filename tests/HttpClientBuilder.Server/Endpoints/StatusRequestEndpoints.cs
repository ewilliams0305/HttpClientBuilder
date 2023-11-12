using System.Net;

namespace HttpClientBuilder.Server.Endpoints;

public static class StatusRequestEndpoints
{
    //public static WebApplication MapStatusEndpoints(this WebApplication app)
    //{
    //    app.MapGet("/api/status/200", () => Results.Ok());
    //    app.MapPost("/api/status/200", () => Results.Ok());
    //    app.MapPut("/api/status/200", () => Results.Ok());
    //    app.MapDelete("/api/status/200", () => Results.Ok());

    //    app.MapGet("/api/status/201", () => Results.Created("api", null));
    //    app.MapPost("/api/status/201", () => Results.Created("api", null));
    //    app.MapPut("/api/status/201", () => Results.Created("api", null));
    //    app.MapDelete("/api/status/201", () => Results.Created("api", null));

    //    app.MapGet("/api/status/400", () => Results.BadRequest());
    //    app.MapPost("/api/status/400", () => Results.BadRequest());
    //    app.MapPut("/api/status/400", () => Results.BadRequest());
    //    app.MapDelete("/api/status/400", () => Results.BadRequest());


    //    app.MapGet("/api/status/404", () => Results.NotFound());
    //    app.MapPost("/api/status/404", () => Results.NotFound());
    //    app.MapPut("/api/status/404", () => Results.NotFound());
    //    app.MapDelete("/api/status/404", () => Results.NotFound());

    //    app.MapGet("/api/status/500", () => Results.StatusCode(500));
    //    app.MapPost("/api/status/500", () => Results.StatusCode(500));
    //    app.MapPut("/api/status/500", () => Results.StatusCode(500));
    //    app.MapDelete("/api/status/500", () => Results.StatusCode(500));

    //    return app;
    //}

    public static WebApplication MapStatusEndpoints(this WebApplication app)
    {

        int[] codes = new int[14]{200,201,202,203,400,401,402,403,404,500,501,502,503,504};

        foreach (var code in codes)
        {
            try
            {
                if (code == 201)
                {
                    app.MapGet($"/api/status/{code}", () => Results.Created("api/weather", null));
                    app.MapPost($"/api/status/{code}", () => Results.Created("api/weather", null));
                    app.MapPut($"/api/status/{code}", () => Results.Created("api/weather", null));
                    app.MapDelete($"/api/status/{code}", () => Results.Created("api/weather", null));
                    continue;
                }

                app.MapGet($"/api/status/{code}", () => Results.StatusCode(code));
                app.MapPost($"/api/status/{code}", () => Results.StatusCode(code));
                app.MapPut($"/api/status/{code}", () => Results.StatusCode(code));
                app.MapDelete($"/api/status/{code}", () => Results.StatusCode(code));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        
        return app;
    }


}