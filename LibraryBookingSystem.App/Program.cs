using LibraryBookingSystem.App.Filters;
using LibraryBookingSystem.Data.Settings;
using MongoDB.Driver;
using MongoDB.Entities;
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

app.Run();
