using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NLog;
using NLog.Web;
using Queuing.Extensions;
using Queuing.Implementation;
using Recipes.Microservice.Common;
using Recipes.Microservice.Common.Interfaces;
using Recipes.Microservice.Common.Services;
using Recipes.Microservice.Common.Services.Storage;
using Recipes.Microservice.Domain.Interfaces;
using Recipes.Microservice.Infrastructure;
using Recipes.Microservice.Queueing.Consumers;
using Recipes.Microservice.Queueing.Models;

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

    builder.Services.AddScoped<IRecipeRepository, RecipeRepository>();
    builder.Services.AddScoped<IIngredientRepository, IngredientRepository>();
    builder.Services.AddScoped<IUserRepository, UserRepository>();
    builder.Services.AddScoped<IUserRecipesRepository, UserRecipesRepository>();
    builder.Services.AddScoped<ICommentRepository, CommentRepository>();
    
    builder.Services.AddScoped<ILoggerService, LoggerService>();
    builder.Services.AddScoped<IFileService, FileService>();
    builder.Services.AddScoped<IFileServiceHandler, LocalFileServiceHandler>();
    
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
    
    builder.Services.AddQueueMessageConsumer<UserChangeQueueMessageConsumer, UserChangeQueueMessage>();
    
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