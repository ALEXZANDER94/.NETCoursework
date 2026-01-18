using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LoginMiddleware.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class LoginMiddleware
    {
        private readonly RequestDelegate _next;

        public LoginMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            // Not a post or not at the "/" path
            if(httpContext.Request.Method != "POST" || httpContext.Request.Path != "/")
            {
                httpContext.Response.StatusCode = 400;
                httpContext.Response.ContentType = "text/html";
                await httpContext.Response.WriteAsync("<p>Invalid Request</p>");
                return;
            }

            StreamReader BodyReader = new StreamReader(httpContext.Request.Body);
            string Body = await BodyReader.ReadToEndAsync();
            string[] Pairs = Body.Split('&');
            Dictionary<string, string> Parameters = new Dictionary<string, string>();

            foreach(string pair in Pairs)
            {
                var tuple = pair.Split('=');
                var key = tuple[0];
                var value = tuple[1];
                Parameters.Add(key, value);
            }
            /* Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(body); */ // faster way to parse the body. returns a Dictionary<string, StringValues>

            //email or password not provided
            if (!Parameters.ContainsKey("email") || !Parameters.ContainsKey("password"))
            {
                httpContext.Response.StatusCode = 400;
                await httpContext.Response.WriteAsync("<p>Invalid input for 'email'</p>");
                await httpContext.Response.WriteAsync("<p>Invalid input for 'password'</p>");
                return;
            }
            string email = Parameters["email"];
            //null or blank email or not valid email format
            if(string.IsNullOrWhiteSpace(email) || !IsValidEmail(email))
            {
                httpContext.Response.StatusCode = 400;
                await httpContext.Response.WriteAsync("<p>Invalid input for 'email'</p>");
                return;
            }

            string password = Parameters["password"];
            //null or blank password
            if(string.IsNullOrWhiteSpace(password))
            {
                httpContext.Response.StatusCode = 400;
                await httpContext.Response.WriteAsync("<p>Invalid input for 'password'</p>");
                return;
            }

            httpContext.Response.StatusCode = (email == "admin@example.com" && password == "admin1234") ? 200 : 400;
            await httpContext.Response.WriteAsync((email == "admin@example.com" && password == "admin1234") ? "<p>Successful login</p>" : "<p>Invalid login</p>");

            await _next(httpContext);
        }

        public bool IsValidEmail(string input)
        {
            try
            {
                var tmpEmailAddress = new MailAddress(input);
                return true;
            }
            catch
            {
                return false;
            }
        }

    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class LoginMiddlewareExtensions
    {
        public static IApplicationBuilder UseLoginMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LoginMiddleware>();
        }
    }
}
