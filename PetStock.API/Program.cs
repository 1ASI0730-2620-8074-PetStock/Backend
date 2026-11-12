using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using PetStock.API.CashShifts.Application.Internal.CommandServices;
using PetStock.API.CashShifts.Application.Internal.QueryServices;
using PetStock.API.CashShifts.Application.Services;
using PetStock.API.CashShifts.Domain.Repositories;
using PetStock.API.CashShifts.Infrastructure.Persistence.EFC.Repositories;
using PetStock.API.Catalog.Application.Internal.CommandServices;
using PetStock.API.Catalog.Application.Internal.QueryServices;
using PetStock.API.Catalog.Application.Services;
using PetStock.API.Catalog.Domain.Repositories;
using PetStock.API.Catalog.Infrastructure.Persistence.EFC.Repositories;
using PetStock.API.IAM.Application.Internal.CommandServices;
using PetStock.API.IAM.Application.Internal.QueryServices;
using PetStock.API.IAM.Application.Services;
using PetStock.API.IAM.Domain.Repositories;
using PetStock.API.IAM.Infrastructure.Persistence.EFC.Repositories;
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
using PetStock.API.Purchasing.Application.Internal.CommandServices;
using PetStock.API.Purchasing.Application.Internal.QueryServices;
using PetStock.API.Purchasing.Application.Services;
using PetStock.API.Purchasing.Domain.Repositories;
using PetStock.API.Purchasing.Infrastructure.Persistence.EFC.Repositories;
using PetStock.API.Analytics.Application.Internal.CommandServices;
using PetStock.API.Analytics.Application.Internal.QueryServices;
using PetStock.API.Analytics.Application.Services;
using PetStock.API.Analytics.Domain.Repositories;
using PetStock.API.Analytics.Infrastructure.Persistence.EFC.Repositories;
using PetStock.API.Profiles.Application.Internal.CommandServices;
using PetStock.API.Profiles.Application.Internal.QueryServices;
using PetStock.API.Profiles.Application.Services;
using PetStock.API.Profiles.Domain.Repositories;
using PetStock.API.Profiles.Infrastructure.Persistence.EFC.Repositories;
using PetStock.API.Sales.Application.Internal.CommandServices;
using PetStock.API.Sales.Application.Internal.QueryServices;
using PetStock.API.Sales.Application.Services;
using PetStock.API.Sales.Domain.Repositories;
using PetStock.API.Sales.Infrastructure.Persistence.EFC.Repositories;
using PetStock.API.Customers.Application.Internal.CommandServices;
using PetStock.API.Customers.Application.Internal.QueryServices;
using PetStock.API.Customers.Application.Services;
using PetStock.API.Customers.Domain.Repositories;
using PetStock.API.Customers.Infrastructure.Persistence.EFC.Repositories;




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
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserCommandService, UserCommandService>();
builder.Services.AddScoped<IUserQueryService, UserQueryService>();
builder.Services.AddScoped<ISessionRepository, SessionRepository>();
builder.Services.AddScoped<ISessionCommandService, SessionCommandService>();
builder.Services.AddScoped<ISessionQueryService, SessionQueryService>();


// Profiles
builder.Services.AddScoped<IUserPreferenceRepository, UserPreferenceRepository>();
builder.Services.AddScoped<IUserPreferenceCommandService, UserPreferenceCommandService>();
builder.Services.AddScoped<IUserPreferenceQueryService, UserPreferenceQueryService>();

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

builder.Services.AddScoped<ISaleRepository, SaleRepository>();
builder.Services.AddScoped<ISaleCommandService, SaleCommandService>();
builder.Services.AddScoped<ISaleQueryService, SaleQueryService>();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<IPaymentCommandService, PaymentCommandService>();
builder.Services.AddScoped<IPaymentQueryService, PaymentQueryService>();


// Customers

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICustomerCommandService, CustomerCommandService>();
builder.Services.AddScoped<ICustomerQueryService, CustomerQueryService>();

builder.Services.AddScoped<IPurchaseHistoryEntryRepository, PurchaseHistoryEntryRepository>();
builder.Services.AddScoped<IPurchaseHistoryEntryCommandService, PurchaseHistoryEntryCommandService>();
builder.Services.AddScoped<IPurchaseHistoryEntryQueryService, PurchaseHistoryEntryQueryService>();

// Purchasing

builder.Services.AddScoped<IPurchaseOrderRepository, PurchaseOrderRepository>();
builder.Services.AddScoped<IPurchaseOrderCommandService, PurchaseOrderCommandService>();
builder.Services.AddScoped<IPurchaseOrderQueryService, PurchaseOrderQueryService>();
builder.Services.AddScoped<IGoodsReceiptRepository, GoodsReceiptRepository>();
builder.Services.AddScoped<IGoodsReceiptCommandService, GoodsReceiptCommandService>();
builder.Services.AddScoped<IGoodsReceiptQueryService, GoodsReceiptQueryService>();

// Cash Shifts
builder.Services.AddScoped<ICashShiftRepository, CashShiftRepository>();
builder.Services.AddScoped<ICashShiftCommandService, CashShiftCommandService>();
builder.Services.AddScoped<ICashShiftQueryService, CashShiftQueryService>();
builder.Services.AddScoped<ICashReconciliationRepository, CashReconciliationRepository>();
builder.Services.AddScoped<ICashReconciliationCommandService, CashReconciliationCommandService>();
builder.Services.AddScoped<ICashReconciliationQueryService, CashReconciliationQueryService>();


// Analytics

builder.Services.AddScoped<IReportRepository, ReportRepository>();
builder.Services.AddScoped<IReportCommandService, ReportCommandService>();
builder.Services.AddScoped<IReportQueryService, ReportQueryService>();

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
