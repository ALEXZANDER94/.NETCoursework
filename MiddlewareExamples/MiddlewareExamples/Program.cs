using MiddlewareExamples.CustomMiddleware;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<MyCustomMiddleware>();
var app = builder.Build();

/* Example of Lambda Middleware */
//app.Run(async (HttpContext context) => {
//    await context.Response.WriteAsync("Hello");
//});

//app.Run(async (HttpContext context) => {
//    await context.Response.WriteAsync("Hello again");
//});

/* Example of Middleware Chaining */
//app.Use(async (HttpContext context, RequestDelegate next) => {
//    await context.Response.WriteAsync("Hello");
//    await next(context);
//});

//app.Run(async (HttpContext context) => {
//    await context.Response.WriteAsync("Hello again");
//});

/* Example of Custom Middleware Class */
app.Use(async (HttpContext context, RequestDelegate next) =>
{
    await context.Response.WriteAsync("From Middleware 1");
    await next(context);
});

//app.UseMiddleware<MyCustomMiddleware>();
//app.UseMyCustomMiddleware();
app.UseHelloCustomMiddleware();

app.Run(async (HttpContext context) =>
{
    await context.Response.WriteAsync("From Middleware 3");
});

app.Run();
