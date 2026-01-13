using System.IO;
using Microsoft.Extensions.Primitives;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Run(async (HttpContext context) =>
{
    /* set status codes and headers */
    //context.Response.Headers["MyKey"] = "My Value";
    //context.Response.Headers["Server"] = "My Server";
    //context.Response.Headers["Content-Type"] = "text/html";
    //context.Response.StatusCode = 400;
    //await context.Response.WriteAsync("<h1>Hello</h1>");
    //await context.Response.WriteAsync(" <h2>World</h2>");

    /* get request information */
    //string path = context.Request.Path;
    //string method = context.Request.Method;
    //context.Response.Headers["Content-Type"] = "text/html";
    //await context.Response.WriteAsync($"<p>Requested Path: {path}</p>");
    //await context.Response.WriteAsync($"<p>Requested Method: {method}</p>");

    /* get query string parameters */
    //if(context.Request.Query.ContainsKey("id"))
    //{
    //   string id = context.Request.Query["id"];
    //    context.Response.Headers["Content-Type"] = "text/html";
    //    await context.Response.WriteAsync($"<p>`id` Query Parameter: {id}</p>");
    //}

    /* get request headers */
    //context.Response.Headers["Content-Type"] = "text/html";
    //if (context.Request.Headers.ContainsKey("AuthorizationKey"))
    //{
    //    string auth_key = context.Request.Headers["AuthorizationKey"];
    //    await context.Response.WriteAsync($"<p>{auth_key}</p>");

    //}

    /* get request body */
    System.IO.StreamReader reader = new StreamReader(context.Request.Body);
    string body = await reader.ReadToEndAsync();

    //convert body to dictionary format
    Dictionary<string, StringValues> queryDict = Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(body);
});

app.Run();
