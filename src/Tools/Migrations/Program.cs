var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddIdentity();

if (args.Contains("--migrate"))
{
	await builder.Services.ExecuteDbMigrationUpdater().ConfigureAwait(false);
}
else if (args.Contains("--migrate-only"))
{
	await builder.Services.ExecuteDbMigrationUpdater().ConfigureAwait(false);
	return;
}

var app = builder.Build();

await app.RunAsync().ConfigureAwait(false);
