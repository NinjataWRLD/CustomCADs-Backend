var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPersistence(builder.Configuration);

builder.Services.AddMediator();
builder.Services.AddMappings();

builder.Services.AddAuthAndJwt(builder.Configuration);

string[] roles = [];
builder.Services.AddRoles(roles);

builder.Services.AddEndpoints();
builder.Services.AddApiDocumentation();

builder.WebHost.AddUploadSizeLimitations();
builder.Services.AddCorsForReact(builder.Configuration);

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "CustomCADs Catalog API v1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();
app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.UseGlobalExceptionHandler();

app.UseEndpoints();
app.MapFallbackToFile("index.html");

await app.RunAsync().ConfigureAwait(false);