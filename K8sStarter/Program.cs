using K8sStarter.Services;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.HttpOverrides;
using K8sStarter;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IKeyVaultService, KeyVaultService>();

// builder.Services.AddHttpsRedirection(options =>
//     {
//         options.HttpsPort = 443;
//     });

GlobalSettings.CloudRoleName = "K8sStarter";
builder.Services.AddApplicationInsightsTelemetry();
builder.Services.AddSingleton<ITelemetryInitializer, CloudRoleNameInitializer>();

var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
app.UseSwagger();
app.UseSwaggerUI();
// }

app.UseForwardedHeaders();

// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
