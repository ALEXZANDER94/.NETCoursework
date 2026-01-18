using System.Xml.Linq;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.UseRouting();

app.UseEndpoints((endpoints) =>
{
    endpoints.Map("countries", async (context) =>
    {
        // List all countries.
        context.Response.ContentType = "text/html";
        await context.Response.WriteAsync("<p>United States</p><p>Canada</p><p>United Kingdom</p><p>India</p><p>Japan</p>");
    });

    endpoints.MapGet("countries/{countryId:int:range(1,100)}", async (context) =>
    {
        int countryId = Convert.ToInt32(context.Request.RouteValues["countryId"]);
        switch (countryId)
        {
            case 1:
                await context.Response.WriteAsync("United States");
                break;
            case 2:
                await context.Response.WriteAsync("Canada");
                break;
            case 3:
                await context.Response.WriteAsync("United Kingdom");
                break;
            case 4:
                await context.Response.WriteAsync("India");
                break;
            case 5:
                await context.Response.WriteAsync("Japan");
                break;
            default:
                await context.Response.WriteAsync("[No Country]");
                break;
        }
    });

    endpoints.MapGet("countries/{countryId:int:min(101)}", async (context) =>
    {
        context.Response.StatusCode = 400;
        await context.Response.WriteAsync("The CountryID should be between 1 and 100");
    });
});

app.MapFallback(async (context) =>
{
    await context.Response.WriteAsync($"Reqiest Received at : {context.Request.Path}");
});

app.Run();
