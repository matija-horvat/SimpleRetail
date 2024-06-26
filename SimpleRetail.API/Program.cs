using Microsoft.OpenApi.Models;
using SimpleRetail.API.Middlewares;
using SimpleRetail.Common;
using SimpleRetail.Common.Middlewares;
using SimpleRetail.Data;
using System.Reflection;
using Microsoft.AspNetCore.SignalR;
using SimpleRetail.API.Hubs;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
var assembly = Assembly.GetExecutingAssembly().GetName().Name;

// Configure Serilog
builder.Host.UseSerilog((hostingContext, loggerConfiguration) =>
{
    loggerConfiguration
        .Enrich.FromLogContext()
        .ReadFrom.Configuration(hostingContext.Configuration)
        .Enrich.WithProperty("Environment", hostingContext.HostingEnvironment.EnvironmentName ?? string.Empty)
        .Enrich.WithProperty("Source", assembly ?? string.Empty);
});

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddData(builder.Configuration);
builder.Services.AddCommon();
builder.Services.AddBL(builder.Configuration);

builder.Services.AddSignalR(); // Register SignalR


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(config =>
{
    config.SwaggerDoc("v1", new OpenApiInfo { Title = assembly, Version = "v1" });

    // Include XML comments from the generated file
    var xmlFile = $"{assembly}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    config.IncludeXmlComments(xmlPath);

    config.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.ApiKey,
        Name = "X-API-KEY",
        In = ParameterLocation.Header,
        Description = "API Key Authentication"
    });
    config.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "ApiKey"
                }
            },
            new string[] {}
        }
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(config =>
    {
        config.SwaggerEndpoint("/swagger/v1/swagger.json", assembly);
    });

}

app.UseExceptionHandler("/error");

app.UseMiddleware<LanguageMiddleware>();
app.UseMiddleware<ApiKeyMiddleware>();
app.UseMiddleware<SerilogMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapHub<SignalRHub>("/myhub"); // Map SignalR hub

app.Run();
