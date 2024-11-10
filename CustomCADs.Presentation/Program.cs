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
builder.Services.AddCatalog(builder.Configuration);

// Add API
builder.Services.AddEndpoints();
builder.Services.AddJsonOptions();
builder.Services.AddApiDocumentation();

// Add Others
builder.Services.AddCorsForClient(builder.Configuration);
builder.WebHost.LimitUploadSize();

var app = builder.Build();

// Use Modules
app.UseAccount();
app.UseAuth();
app.UseCatalog();

// Use API
app.UseEndpoints();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger("CustomCADs API");
}

await app.RunAsync().ConfigureAwait(false);