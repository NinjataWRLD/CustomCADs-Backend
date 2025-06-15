using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using CustomCADs.Shared.Abstractions.Cache;
using CustomCADs.Shared.Abstractions.Delivery;
using CustomCADs.Shared.Abstractions.Email;
using CustomCADs.Shared.Abstractions.Events;
using CustomCADs.Shared.Abstractions.Payment;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.Abstractions.Storage;
using CustomCADs.Shared.Abstractions.Tokens;
using CustomCADs.Shared.Infrastructure.Cache;
using CustomCADs.Shared.Infrastructure.Delivery;
using CustomCADs.Shared.Infrastructure.Email;
using CustomCADs.Shared.Infrastructure.Events;
using CustomCADs.Shared.Infrastructure.Payment;
using CustomCADs.Shared.Infrastructure.Requests;
using CustomCADs.Shared.Infrastructure.Storage;
using CustomCADs.Shared.Infrastructure.Tokens;
using FluentValidation;
using JasperFx.CodeGeneration;
using Microsoft.Extensions.Options;
using System.Reflection;
using Wolverine;
using Wolverine.FluentValidation;

#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
	public static void AddCacheService(this IServiceCollection services)
	{
		services.AddMemoryCache();
		services.AddScoped(typeof(ICacheService<>), typeof(MemoryCacheService<>));
		services.AddScoped<ICacheService, MemoryCacheService>();
	}

	public static void AddEmailService(this IServiceCollection services)
	{
		services.AddScoped<IEmailService, FluentEmailService>();
	}

	public static void AddTokensService(this IServiceCollection services)
	{
		services.AddScoped<ITokenService, TokenService>();
	}

	public static void AddMessagingServices(this IServiceCollection services, bool codeGen, Assembly entry, params Assembly[] assemblies)
	{
		services.AddValidatorsFromAssemblies(assemblies);

		services.AddWolverine(cfg =>
		{
			foreach (Assembly assembly in assemblies)
			{
				cfg.Discovery.IncludeAssembly(assembly);
			}

			if (codeGen)
			{
				cfg.CodeGeneration.ApplicationAssembly = entry;
				cfg.CodeGeneration.TypeLoadMode = TypeLoadMode.Static;
			}

			cfg.UseFluentValidation();
		});

		services.AddScoped<IRequestSender, WolverineRequestSender>();
		services.AddScoped<IEventRaiser, WolverineEventRaiser>();
	}

	public static void AddPaymentService(this IServiceCollection services)
	{
		services.AddScoped<Stripe.PaymentIntentService>();
		services.AddScoped<IPaymentService, StripeService>();
	}

	public static void AddStorageService(this IServiceCollection services)
	{
		services.AddSingleton<IAmazonS3>(sp =>
		{
			var settings = sp.GetRequiredService<IOptions<StorageSettings>>().Value;

			AmazonS3Config config = new()
			{
				RegionEndpoint = RegionEndpoint.GetBySystemName(settings.Region),
			};

			BasicAWSCredentials credentials = new(settings.AccessKey, settings.SecretKey);
			return new AmazonS3Client(credentials, config);
		});
		services.AddScoped<IStorageService, AmazonS3Service>();
	}

	public static void AddDeliveryService(this IServiceCollection services)
	{
		services.AddDeliveryShipmentService();
		services.AddDeliveryPrintService();
		services.AddDeliveryTrackService();
		services.AddDeliveryPickupService();
		services.AddDeliveryLocationService();
		services.AddDeliveryCalculationService();
		services.AddDeliveryClientService();
		services.AddDeliveryValidationService();
		services.AddDeliveryServicesService();
		services.AddDeliveryPaymentService();

		services.AddScoped<IDeliveryService, SpeedyService>();
	}
}
