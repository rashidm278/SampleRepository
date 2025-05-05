using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SearchProject;
using SearchProject.Application.Handlers;
using SearchProject.Domain.Dtos;
using SearchProject.Domain.Interfaces;
using SearchProject.Middleware;
using SearchProject.Repository.Data;
using SearchProject.Repository.Repositories;
using Serilog;
using System.Text;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
        policy.AllowAnyMethod()
              .AllowAnyHeader()
              .SetIsOriginAllowed(_ => true)
              .AllowCredentials());

    options.AddPolicy("AllowSpecific", policy =>
    {
        var config = builder.Configuration;
        policy.WithOrigins(config.GetSection("Cors:Origins").Get<string[]>() ?? Array.Empty<string>())
              .WithHeaders(config.GetSection("Cors:Headers").Get<string[]>() ?? Array.Empty<string>())
              .WithMethods(config.GetSection("Cors:Methods").Get<string[]>() ?? Array.Empty<string>());
    });

    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddMemoryCache();
builder.Services.AddRateLimiter(options =>
{
    options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
        RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: httpContext.User.Identity?.Name ?? httpContext.Connection.RemoteIpAddress?.ToString() ?? "anonymous",
            factory: _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 100,
                Window = TimeSpan.FromMinutes(1),
                QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                QueueLimit = 0
            }));

    options.RejectionStatusCode = 429;
});
builder.Services.AddResponseCaching();
builder.Services.Configure<AuthSettings>(
    builder.Configuration.GetSection("Jwt"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddMediatR(typeof(LoginCommandHandler).Assembly);
builder.Services.AddTransient<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<IErrorReportRepository, ErrorReportRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>(); 
builder.Services.AddScoped<ISearchHistoryRepository, SearchHistoryRepository>();
builder.Services.AddSwaggerGen(c =>
 {
     c.SwaggerDoc("v1", new OpenApiInfo
     {
         Version = "1.0",
         Title = "SEARCH PROJECT",
         Description = "A ASP .NET Core Project for search",
     });
     c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
     {
         Description = @"JWT Authorization header using the Bearer scheme.",
         Name = "Authorization",
         In = ParameterLocation.Header,
         Type = SecuritySchemeType.ApiKey,
         Scheme = "Bearer"
     });
     c.AddSecurityRequirement(new OpenApiSecurityRequirement
             {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
             });
     var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
     var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
     c.IncludeXmlComments(xmlPath);
 });

var jwtSettings = builder.Configuration.GetSection("Jwt");
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]))
        };
    });


var app = builder.Build();

app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Search Project 1.0");
    c.RoutePrefix = string.Empty;
});

app.UseResponseCaching();

app.UseHttpsRedirection();

app.UseRouting();

app.UseMiddleware<LoggingMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
