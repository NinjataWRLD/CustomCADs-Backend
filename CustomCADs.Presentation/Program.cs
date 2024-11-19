using static CustomCADs.Shared.Core.Constants.Roles;

var builder = WebApplication.CreateBuilder(args);

// Add AuthN & AuthZ
builder.Services.AddAuthN().AddJwt(builder.Configuration);
builder.Services.AddAuthZ([Client, Contributor, Designer, Admin]);

// Add External services
builder.Services.AddCache();
builder.Services.AddRaiser();
builder.Services.AddEmail(builder.Configuration);
builder.Services.AddPayment(builder.Configuration);
builder.Services.AddStorage(builder.Configuration);

// Add Core Services and Use Cases
builder.Services.AddSignInService();
builder.Services.AddUseCases();

// Add Modules
builder.Services.AddAccount(builder.Configuration);
builder.Services.AddAuth(builder.Configuration);
builder.Services.AddCadsPersistence(builder.Configuration);
builder.Services.AddCatalog(builder.Configuration);
builder.Services.AddGallery(builder.Configuration);
builder.Services.AddOrders(builder.Configuration);
builder.Services.AddDeliveryPersistence(builder.Configuration);

// Add API
builder.Services.AddEndpoints();
builder.Services.AddJsonOptions();
builder.Services.AddApiDocumentation();

// Add Others
builder.Services.AddCorsForClient(builder.Configuration);
builder.Services.AddProblemDetails();
builder.WebHost.LimitUploadSize();

var app = builder.Build();

// Global Exception Filters
app.UseExceptionHandler();

// API
app.UseStaticFiles();
app.UseEndpoints();
if (true) // Maybe I'll return 'app.Environment.IsDevelopment()' one day
{
    app.MapApiDocumentationUi(
        apiPattern: "/openapi/{documentName}.json",
        uiPattern: "/scalar/{documentName}"
    ).MapApiDocumentationUi(
        apiPattern: "/{documentName}.json",
        uiPattern: "/{documentName}"
    );
}

await app.RunAsync().ConfigureAwait(false);