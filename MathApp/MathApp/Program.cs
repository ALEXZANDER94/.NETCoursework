using System.IO;
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Run(async (HttpContext context) =>
{
    var request = context.Request.Query;
    if(!request.ContainsKey("firstNumber") || !request.ContainsKey("secondNumber") || !request.ContainsKey("operation"))
    {
        context.Response.StatusCode = 404;
        await context.Response.WriteAsync("Missing required parameters");
        return;
    }

    if(! (int.TryParse(request["firstNumber"], out var firstNumber)))
    {
        context.Response.StatusCode = 404;
        await context.Response.WriteAsync("Missing required parameters");
        return;
    }
    if (!(int.TryParse(request["secondNumber"], out var secondNumber)))
    {
        context.Response.StatusCode = 404;
        await context.Response.WriteAsync("Missing required parameters");
        return;
    }
    var operation = request["operation"];

    switch(operation)
    {
        case "add":
            await context.Response.WriteAsync(string.Format("{0} + {1} = {2}", firstNumber, secondNumber, (firstNumber + secondNumber)));
            break;

        case "sub":
        case "subtract":
            await context.Response.WriteAsync(string.Format("{0} - {1} = {2}", firstNumber, secondNumber, (firstNumber - secondNumber)));
            break;

        case "mult":
        case "multiply":
            await context.Response.WriteAsync(string.Format("{0} x {1} = {2}", firstNumber, secondNumber, (firstNumber * secondNumber)));
            break;

        case "div":
        case "divide":
            await context.Response.WriteAsync(string.Format("{0} ÷ {1} = {2}", firstNumber, secondNumber, (float) (firstNumber / secondNumber)));
            break;
        default:
            context.Response.StatusCode = 404;
            await context.Response.WriteAsync("Missing required parameters");
            break;
    }
    return;
});

app.Run();
