using static CustomCADs.Shared.Core.Constants.Roles;

var builder = WebApplication.CreateBuilder(args);

// Neccessities
builder.Services.AddCorsForClient(builder.Configuration);
builder.Services.AddAuthN().AddJwt(builder.Configuration);
builder.Services.AddAuthZ([Client, Contributor, Designer, Admin]);

// Use Cases
builder.Services.AddUseCases();
builder.Services.AddCacheService();
builder.Services.AddRaiserService();

// External Services 
builder.Services.AddEmailService(builder.Configuration);
builder.Services.AddPaymentService(builder.Configuration);
builder.Services.AddDeliveryService(builder.Configuration);
builder.Services.AddStorageService(builder.Configuration);

// Modules
builder.Services.AddAccounts(builder.Configuration);
builder.Services.AddCarts(builder.Configuration);
builder.Services.AddCatalog(builder.Configuration);
builder.Services.AddCategories(builder.Configuration);
builder.Services.AddDelivery(builder.Configuration);
builder.Services.AddFiles(builder.Configuration);
builder.Services.AddIdentity(builder.Configuration);
builder.Services.AddOrders(builder.Configuration);
builder.Services.AddGlobalExceptionHandler();

// Database Updater
if (args.Contains("--migrate"))
{
    await builder.Services.AddDbMigrationUpdater().ConfigureAwait(false);
}
else if (args.Contains("--migrate-only"))
{
    await builder.Services.AddDbMigrationUpdater().ConfigureAwait(false);
    return;
}

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

await app.RunAsync().ConfigureAwait(false);