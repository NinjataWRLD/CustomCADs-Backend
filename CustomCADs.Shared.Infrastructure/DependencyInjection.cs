using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using CustomCADs.Shared.Application.Cache;
using CustomCADs.Shared.Application.Email;
using CustomCADs.Shared.Application.Events;
using CustomCADs.Shared.Application.Payment;
using CustomCADs.Shared.Application.Storage;
using CustomCADs.Shared.Infrastructure.Cache;
using CustomCADs.Shared.Infrastructure.Email;
using CustomCADs.Shared.Infrastructure.Events;
using CustomCADs.Shared.Infrastructure.Payment;
using CustomCADs.Shared.Infrastructure.Storage;
using Microsoft.Extensions.Options;
using Stripe;
using System.Reflection;
using Wolverine;

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
        services.AddScoped<IEmailService, MailKitService>();
    }

    public static void AddEventRaiser(this IServiceCollection services, params Assembly[] assemblies)
    {
        services.AddWolverine(cfg =>
        {
            foreach (Assembly assembly in assemblies)
            {
                cfg.Discovery.IncludeAssembly(assembly);
            }
        });
        services.AddScoped<IEventRaiser, WolverineEventRaiser>();
    }

    public static void AddPaymentService(this IServiceCollection services)
    {
        services.AddScoped<PaymentIntentService>();
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
    }
}
