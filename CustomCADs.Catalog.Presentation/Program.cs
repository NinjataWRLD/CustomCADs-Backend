var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPersistence(builder.Configuration);

builder.Services.AddUseCases();
builder.Services.AddMappings();

builder.Services.AddAuthAndJwt(builder.Configuration);

string[] roles = [];
builder.Services.AddRoles(roles);

builder.Services.AddEndpoints();

builder.WebHost.AddUploadSizeLimitations();
builder.Services.AddCorsForReact(builder.Configuration);

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger("CustomCADs Catalog API v1");
}

app.UseHttpsRedirection();
app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.UseGlobalExceptionHandler();

app.UseEndpoints();
app.MapFallbackToFile("index.html");

await app.RunAsync().ConfigureAwait(false);