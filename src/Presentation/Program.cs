using CustomCADs.Presentation;
using static CustomCADs.Shared.Core.Constants.Roles;

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
builder.Services.AddIdentity();
builder.Services.AddGlobalExceptionHandler();

// API
builder.Services.AddEndpoints();
builder.Services.AddJsonOptions();
builder.Services.AddApiDocumentation();
builder.Services.AddProblemDetails();
builder.WebHost.LimitUploadSize();

var app = builder.Build();

// Stuff
app.UseCorsForClient();
app.UseAuthentication();
app.UseAuthorization();

app.UseExceptionHandler();
app.UseJwtPrincipal();
app.UseCsrfProtection();
app.UseStaticFiles();

// API & Documentation
app.UseEndpoints();
app.MapApiDocumentationUi(
	apiPattern: "/openapi/{documentName}.json",
	uiPattern: "/scalar"
).MapApiDocumentationUi(
	apiPattern: "/swagger/{documentName}.json",
	uiPattern: "/swagger"
);
app.MapStripeWebhook();

await app.RunAsync().ConfigureAwait(false);
