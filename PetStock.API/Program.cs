using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using PetStock.API.Catalog.Application.Internal.CommandServices;
using PetStock.API.Catalog.Application.Internal.QueryServices;
using PetStock.API.Catalog.Application.Services;
using PetStock.API.Catalog.Domain.Repositories;
using PetStock.API.Catalog.Infrastructure.Persistence.EFC.Repositories;
using PetStock.API.Resources;
using PetStock.API.Shared.Domain.Repositories;
using PetStock.API.Shared.Infrastructure.Interfaces.ASP.Configuration;
using PetStock.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using PetStock.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Scalar.AspNetCore;
using PetStock.API.Inventory.Application.Internal.CommandServices;
using PetStock.API.Inventory.Application.Internal.QueryServices;
using PetStock.API.Inventory.Application.Services;
using PetStock.API.Inventory.Domain.Repositories;
using PetStock.API.Inventory.Infrastructure.Persistence.EFC.Repositories;
var builder = WebApplication.CreateBuilder(args);

// Lower case URLs
builder.Services.AddRouting(options => options.LowercaseUrls = true);

// Localization (.resx files)
builder.Services.AddLocalization();

// Controllers with kebab-case routes and translated validation messages
builder.Services.AddControllers(options => options.Conventions.Add(new KebabCaseRouteNamingConvention()))
    .AddDataAnnotationsLocalization(options =>
        options.DataAnnotationLocalizerProvider = (_, factory) => factory.Create(typeof(SharedResource)))
    .ConfigureApiBehaviorOptions(options =>
    {
        options.InvalidModelStateResponseFactory = context =>
        {
            var localizer = context.HttpContext.RequestServices
                .GetRequiredService<IStringLocalizer<SharedResource>>();
            var problemDetailsFactory = context.HttpContext.RequestServices
                .GetRequiredService<ProblemDetailsFactory>();
            var problemDetails = problemDetailsFactory.CreateValidationProblemDetails(
                context.HttpContext,
                context.ModelState,
                statusCode: StatusCodes.Status400BadRequest,
                title: localizer["InvalidModelStateTitle"].Value);
            var result = new BadRequestObjectResult(problemDetails);
            result.ContentTypes.Add("application/problem+json");
            return result;
        };
    });

// Translated messages for unexpected 500 errors
builder.Services.AddProblemDetails(options =>
{
    options.CustomizeProblemDetails = context =>
    {
        if (context.ProblemDetails.Status is null or >= 500)
        {
            var localizer =
                context.HttpContext.RequestServices.GetRequiredService<IStringLocalizer<SharedResource>>();
            context.ProblemDetails.Title ??= localizer["UnexpectedServerError"].Value;
            context.ProblemDetails.Detail ??= localizer["UnexpectedErrorProcessingRequest"].Value;
        }
    };
});

// OpenAPI document
builder.Services.AddOpenApi();

// CORS so the frontend can call the API
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllPolicy", policy =>
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

// MySQL database connection
builder.Services.AddDbContext<AppDbContext>((serviceProvider, options) =>
{
    var connectionStringTemplate = builder.Configuration.GetConnectionString("DefaultConnection");
    if (string.IsNullOrWhiteSpace(connectionStringTemplate))
        throw new InvalidOperationException("Database connection string is not set in the configuration.");

    // Replaces %VARIABLE% values in production
    var connectionString = Environment.ExpandEnvironmentVariables(connectionStringTemplate);
    if (string.IsNullOrWhiteSpace(connectionString))
        throw new InvalidOperationException("Database connection string is not set in the configuration.");

    options.UseMySQL(connectionString)
        .UseLoggerFactory(serviceProvider.GetRequiredService<ILoggerFactory>())
        .EnableDetailedErrors();

    if (builder.Environment.IsDevelopment())
        options.EnableSensitiveDataLogging();
});

// Shared dependency injection
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Bounded contexts dependency injection
// (each bounded context adds its repositories and services under its own comment)

// IAM

// Profiles

// Catalog
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryCommandService, CategoryCommandService>();
builder.Services.AddScoped<ICategoryQueryService, CategoryQueryService>();
builder.Services.AddScoped<ISupplierRepository, SupplierRepository>();
builder.Services.AddScoped<ISupplierCommandService, SupplierCommandService>();
builder.Services.AddScoped<ISupplierQueryService, SupplierQueryService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductCommandService, ProductCommandService>();
builder.Services.AddScoped<IProductQueryService, ProductQueryService>();
// Inventory
builder.Services.AddScoped<IProductStockRepository, ProductStockRepository>();
builder.Services.AddScoped<IProductStockCommandService, ProductStockCommandService>();
builder.Services.AddScoped<IProductStockQueryService, ProductStockQueryService>();
builder.Services.AddScoped<IStockAlertRepository, StockAlertRepository>();
builder.Services.AddScoped<IStockAlertCommandService, StockAlertCommandService>();
builder.Services.AddScoped<IStockAlertQueryService, StockAlertQueryService>();
// Sales

// Customers

// Purchasing

// Cash Shifts

// Analytics

var app = builder.Build();

// Apply pending migrations on startup
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.Migrate();
}

app.UseExceptionHandler();

// Cultures read from the Accept-Language header (English by default)
string[] supportedCultures = ["en", "en-US", "es", "es-PE"];
var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture(supportedCultures[0])
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);
localizationOptions.ApplyCurrentCultureToResponseHeaders = true;
app.UseRequestLocalization(localizationOptions);

app.UseCors("AllowAllPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

// OpenAPI and Scalar in every environment
app.MapOpenApi();
app.MapScalarApiReference();

app.MapControllers();

app.Run();
