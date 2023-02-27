using K8sStarter.Services;
using Microsoft.AspNetCore.HttpOverrides;

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
