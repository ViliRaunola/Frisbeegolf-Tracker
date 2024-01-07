﻿using Amazon.Runtime.Internal;
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

        public async Task InvokeAsync(HttpContext context)
        {
            try 
            {
                string authHeader = context.Request.Headers["Authorization"];

                if (authHeader != null)
                {
                    string token = authHeader.ToString().Remove(0, 7);
                    GoogleJsonWebSignature.Payload payload = await GoogleJsonWebSignature.ValidateAsync(token);

                    if (payload != null)
                    {
                        Console.WriteLine(payload);
                        context.Items["User"] = payload;
                    }

                    Console.WriteLine("Here");
                }
                System.Diagnostics.Debug.WriteLine("Here");

                await _next(context);
            }
            catch(System.Exception ex) 
            {
                Console.WriteLine("invalid google token");
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
