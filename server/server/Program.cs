using Microsoft.AspNetCore.Authentication.Cookies;
using MongoDB.Driver;
using server.Models;
using server.Services;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options => {
    options.SuppressAsyncSuffixInActionNames = false; // Now .NET will not remove the ...Async name from the methods
});

builder.Services.Configure<UsersDatabaseSettings>(
    builder.Configuration.GetSection("MongoDatabase"));

builder.Services.Configure<MapsDatabaseSettings>(
    builder.Configuration.GetSection("MongoDatabase"));

builder.Services.AddSingleton<UsersService>();
builder.Services.AddSingleton<MapsService>();


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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//app.UseAuthentication();

app.Run();
