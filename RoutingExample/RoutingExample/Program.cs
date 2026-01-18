using RoutingExample.CustomConstraints;
using System.Xml.Linq;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRouting(options =>
{
    options.ConstraintMap.Add("months", typeof(MonthCustomConstraint));
});


var app = builder.Build();

app.Map("map1", async (context) =>
{
    await context.Response.WriteAsync("In Map 1");
});

app.Map("map2", async (context) =>
{
    await context.Response.WriteAsync("In Map 2");
});

/* Routing with routing parameters */
app.Map("files/{filename}.{extension}", async (context) =>
{
    string? fileName = Convert.ToString(context.Request.RouteValues["filename"]);
    string? fileExtension = Convert.ToString(context.Request.RouteValues["extension"]);
    await context.Response.WriteAsync($"In Files : {fileName}.{fileExtension}");
});

app.Map("employee/profile/{employeename=david}", async (context) =>
{
    string? employeeName = Convert.ToString(context.Request.RouteValues["employeename"]);
    await context.Response.WriteAsync($"In Employee Profile : {employeeName}");
});

/* Routing with optional parameters */
app.Map("product/details/{id?}", async (context) =>
{
    if(context.Request.RouteValues.ContainsKey("id"))
    {
        int? id = Convert.ToInt32(context.Request.RouteValues["id"]);
        await context.Response.WriteAsync($"In Product Details : {id}");
    }
    else
    {
        await context.Response.WriteAsync($"In Product Details : id is not supplied");
    }
});

/* Routing with type-constrained parameters */
app.Map("cities/{cityid:guid}", async (context) =>
{
    Guid cityid = Guid.Parse(Convert.ToString(context.Request.RouteValues["cityid"])!);
    await context.Response.WriteAsync($"In Cities : {cityid}");
});

/* Routing with length-constrained parameters */
app.Map("employee/profile/{employeename:length(4,7)=david}", async (context) =>
{
    string? employeeName = Convert.ToString(context.Request.RouteValues["employeename"]);
    await context.Response.WriteAsync($"In Employee Profile : {employeeName}");
});
app.Map("product/details/{id:int:range(1,1000)?}", async (context) =>
{
    if (context.Request.RouteValues.ContainsKey("id"))
    {
        int? id = Convert.ToInt32(context.Request.RouteValues["id"]);
        await context.Response.WriteAsync($"In Product Details : {id}");
    }
    else
    {
        await context.Response.WriteAsync($"In Product Details : id is not supplied");
    }
});

/* Routing with a custom Constraint class */
app.Map("sales-report/{year:int:min(1900)}/{month:months}", async (context) =>
{
    int year = Convert.ToInt32(context.Request.RouteValues["year"]);
    string? month = Convert.ToString(context.Request.RouteValues["month"]);

    await context.Response.WriteAsync($"In Sales Report : {year} - {month}");
});

app.MapFallback(async (context) =>
{
    await context.Response.WriteAsync($"Request Received at: {context.Request.Path}");
});

app.Run();
