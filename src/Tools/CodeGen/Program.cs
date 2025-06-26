using JasperFx;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("codegensettings.json");

// Use Cases
builder.Services.GenerateUseCases(builder.Environment);
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

var app = builder.Build();
return await app.RunJasperFxCommands(args).ConfigureAwait(false);
