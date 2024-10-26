using static CustomCADs.Shared.Domain.Constants;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMessageBus();
builder.Services.AddIdentityAuth(builder.Configuration);

builder.Services.AddEmail(builder.Configuration);
builder.Services.AddEndpoints();

builder.Services.AddApiDocumentation();
builder.Services.AddRoles([Client, Contributor, Designer, Admin]);

var app = builder.Build();

app.UseGlobalExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseEndpoints();

app.Run();