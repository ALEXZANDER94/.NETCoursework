var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers(options =>
{
});
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.UseRouting();
app.MapControllers();

app.Run();
