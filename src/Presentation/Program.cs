using CustomCADs.Presentation;
using static CustomCADs.Shared.Domain.Constants.Roles;

var builder = WebApplication.CreateBuilder(args);

// Neccessities
builder.Services.AddCorsForClient(builder.Configuration);
builder.Services.AddAuthN().AddJwt(builder.Configuration);
builder.Services.AddAuthZ([Customer, Contributor, Designer, Admin]);

// Use Cases
builder.Services.AddUseCases(builder.Environment);
builder.Services.AddCache();
builder.Services.AddBackgroundJobs();

// External Services
builder.Services.AddEmailService(builder.Configuration);
builder.Services.AddTokensService(builder.Configuration);
builder.Services.AddPaymentService(builder.Configuration);
builder.Services.AddDeliveryService(builder.Configuration);
builder.Services.AddStorageService(builder.Configuration);

// Modules
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddIdentity(builder.Configuration);
builder.Services.AddDomainServices();
builder.Services.AddGlobalExceptionHandler();

// API
builder.Services.AddEndpoints();
builder.Services.AddJsonOptions();
builder.Services.AddApiDocumentation();
builder.Services.AddRateLimiting();
builder.Services.AddProblemDetails();
builder.Services.AddHealthChecks();

var app = builder.Build();

// Neccessities
app.UseCorsForClient();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();

// Global Middleware
app.UseExceptionHandler();
app.UseJwtPrincipal();
app.UseCsrfProtection();
app.UseIdempotencyKeys();

// API & Documentation
app.UseEndpoints();
app.UseRateLimiter();
app.MapApiDocumentationUi(
	apiPattern: "/openapi/{documentName}.json",
	uiPattern: "/scalar"
).MapApiDocumentationUi(
	apiPattern: "/swagger/{documentName}.json",
	uiPattern: "/swagger"
);
app.MapStripeWebhook();
app.MapHealthChecks("/health");

await app.RunAsync().ConfigureAwait(false);
