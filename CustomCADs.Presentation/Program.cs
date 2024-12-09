using static CustomCADs.Shared.Core.Constants.Roles;

var builder = WebApplication.CreateBuilder(args);

// Add AuthN & AuthZ
builder.Services.AddAuthN().AddJwt(builder.Configuration);
builder.Services.AddAuthZ([Client, Contributor, Designer, Admin]);

// Add Use Cases and External services 
builder.Services.AddUseCases();
builder.Services.AddCache();
builder.Services.AddRaiser();
builder.Services.AddEmail(builder.Configuration);
builder.Services.AddPayment(builder.Configuration);
builder.Services.AddStorage(builder.Configuration);

// Add Modules
builder.Services.AddAccounts(builder.Configuration);
builder.Services.AddCads(builder.Configuration);
builder.Services.AddCarts(builder.Configuration);
builder.Services.AddCatalog(builder.Configuration);
builder.Services.AddCategories(builder.Configuration);
builder.Services.AddDelivery(builder.Configuration);
builder.Services.AddIdentity(builder.Configuration);
builder.Services.AddOrders(builder.Configuration);

// Add Database Updater
if (args.Contains("--migrate"))
{
    await builder.Services.AddDbMigrationUpdater().ConfigureAwait(false);
} 
else if (args.Contains("--migrate-only"))
{
    await builder.Services.AddDbMigrationUpdater().ConfigureAwait(false);
    return;
}

// Add API
builder.Services.AddEndpoints();
builder.Services.AddJsonOptions();
builder.Services.AddApiDocumentation();

// Add Stuff
builder.Services.AddCorsForClient(builder.Configuration);
builder.Services.AddProblemDetails();
builder.WebHost.LimitUploadSize();

var app = builder.Build();

// Use Stuff
app.UseExceptionHandler();
app.UseJwtPrincipal();
app.UseStaticFiles();

// Use API & Map Documentation
app.UseEndpoints();
app.MapApiDocumentationUi(
    apiPattern: "/openapi/{documentName}.json",
    uiPattern: "/scalar/{documentName}"
).MapApiDocumentationUi(
    apiPattern: "/swagger/{documentName}.json",
    uiPattern: "/swagger/{documentName}"
);

await app.RunAsync().ConfigureAwait(false);