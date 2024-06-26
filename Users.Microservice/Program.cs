using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NLog;
using NLog.Web;
using Queuing.Extensions;
using Queuing.Implementation;
using Users.Microservice.Common;
using Users.Microservice.Common.ExternalServices.GoogleGate;
using Users.Microservice.Common.Interfaces;
using Users.Microservice.Common.Services;
using Users.Microservice.Domain.Interfaces;
using Users.Microservice.Infrastructure;

var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
try
{
    var builder = WebApplication.CreateBuilder(args);

    // Configure services.
    builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
    builder.Services.AddAutoMapper(typeof(Program));
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

    var jwtSection = builder.Configuration.GetSection(Constants.JwtConfigurationSectionKeys.Jwt);
    builder.Services.AddScoped<IJwtHandler, JwtHandler>();
    builder.Services.AddScoped<IOAuthProxy, OAuthProxy>();
    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSection[Constants.JwtConfigurationSectionKeys.ValidIssuer],
            ValidAudience = jwtSection[Constants.JwtConfigurationSectionKeys.ValidAudience],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(jwtSection[Constants.JwtConfigurationSectionKeys.SecurityKey] ?? string.Empty))
        };
    });

    builder.Services.AddScoped<IUserRepository, UserRepository>();
    
    builder.Services.AddScoped<ILoggerService, LoggerService>();
    
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddRouting(options => options.LowercaseUrls = true);

    builder.Services.AddQueueing(new QueueingConfigurationSettings
    {
        Username = "guest",
        Password = "guest",
        Hostname = "localhost",
        Port = 5672,
        ConsumerConcurrency = 5
    });
    
    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    logger.Error(ex, "Stopped program on initialization.");
    throw;
}
finally
{
    LogManager.Shutdown();
}