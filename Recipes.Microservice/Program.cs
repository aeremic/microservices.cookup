using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NLog;
using NLog.Web;
using Recipes.Microservice.Common;
using Recipes.Microservice.Common.Services;
using Recipes.Microservice.Infrastructure;

var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
try
{
    var builder = WebApplication.CreateBuilder(args);

    // Configure services.
    builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
    builder.Services.AddAutoMapper(typeof(Program));
    builder.Services.AddDbContext<Repository>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

    var jwtSection = builder.Configuration.GetSection(Constants.JwtConfigurationSectionKeys.Jwt);
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

    builder.Services.AddScoped<FileService>();
    
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddRouting(options => options.LowercaseUrls = true);

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
    app.UseStaticFiles();

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