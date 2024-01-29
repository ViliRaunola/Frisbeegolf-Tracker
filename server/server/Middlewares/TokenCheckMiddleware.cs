using Amazon.Runtime.Internal;
using Google.Apis.Auth;
using Google.Apis.Auth.OAuth2;
using MongoDB.Driver.Core.WireProtocol.Messages;
using Newtonsoft.Json.Linq;
using server.Models;
using server.Services;

namespace server.Middlewares
{
    public class TokenCheckMiddleware
    {

        private readonly RequestDelegate _next;

        public TokenCheckMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        //TODO: should check more than just the token. Check Google login API documentation for all of the checks.
        public async Task InvokeAsync(HttpContext context)
        {
            try 
            {
                string authHeader = context.Request.Headers["Authorization"];

                if (authHeader != null)
                {
                    string token = authHeader.ToString().Remove(0, 6);
                    Console.WriteLine(authHeader.ToString());
                    GoogleJsonWebSignature.Payload payload = await GoogleJsonWebSignature.ValidateAsync(token);
                    Console.WriteLine(payload);
                    if (payload != null)
                    {
                        context.Items["User"] = payload.Subject;
                    }  
                }
                await _next(context);
            }
            catch(System.Exception ex) 
            {
                Console.WriteLine("Invalid google token");
            }
           
        }
    }

    public static class TokenCheckMiddlewareExtensions
    {
        public static IApplicationBuilder UseTokenCheckMiddleware(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<TokenCheckMiddleware>();
        }
    }
}
