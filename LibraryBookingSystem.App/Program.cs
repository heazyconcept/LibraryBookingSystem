using LibraryBookingSystem.App.Filters;
using LibraryBookingSystem.App.Middlewares;
using LibraryBookingSystem.Common;
using LibraryBookingSystem.Core;
using LibraryBookingSystem.Data.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using MongoDB.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
var appSettingsSection = builder.Configuration.GetSection("AppSettings");

var appSettings = appSettingsSection.Get<AppSettings>();
await DB.InitAsync(appSettings.DatabaseName,
    MongoClientSettings.FromConnectionString(appSettings.ConnectionString));

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(opt =>
    {
        opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<AppSettings>(appSettingsSection);
builder.Services.AddTransient<UserSessionFilters>();
builder.Services.AddTransient<AdminUserSerssionFilter>();
builder.Services.AddDomainService();
builder.Services.AddCommonService();
builder.Services.AddLogging(logging =>
{
    logging.AddConsole();
    logging.AddDebug();
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = appSettings.JWT.Issuer,
            ValidAudience = appSettings.JWT.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettings.JWT.Key)),
            RoleClaimType = "role"
        };
       
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
var loggerFactory = app.Services.GetRequiredService<ILoggerFactory>();
app.UseCustomExceptionHandler();
app.ConfigureCommonApp(loggerFactory);
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
