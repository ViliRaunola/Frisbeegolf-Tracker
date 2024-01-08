using Microsoft.AspNetCore.Authentication.Cookies;
using MongoDB.Driver;
using server.Middlewares;
using server.Models;
using server.Services;


var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddControllers(options => {
    options.SuppressAsyncSuffixInActionNames = false; // Now .NET will not remove the ...Async name from the methods
});

builder.Services.Configure<UsersDatabaseSettings>(
    builder.Configuration.GetSection("MongoDatabase"));

builder.Services.Configure<MapsDatabaseSettings>(
    builder.Configuration.GetSection("MongoDatabase"));

builder.Services.AddSingleton<UsersService>();
builder.Services.AddSingleton<MapsService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
            ;
        });
});


//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//})
//    .AddCookie(options =>
//    {
//        options.LoginPath = "/account/google-login";
//    })
//    .AddGoogle(options =>
//    {
//        options.ClientId = "";
//        options.ClientSecret = "";
//    });

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddScoped<TokenCheckMiddleware>();

var app = builder.Build();

app.UseCors(MyAllowSpecificOrigins);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


//app.UseAuthorization();

app.UseMiddleware<TokenCheckMiddleware>();

app.MapControllers();


//app.UseAuthentication();
app.Run();
