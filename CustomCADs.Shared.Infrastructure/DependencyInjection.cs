using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using CustomCADs.Shared.Core.Email;
using CustomCADs.Shared.Core.Payment;
using CustomCADs.Shared.Core.Storage;
using CustomCADs.Shared.Infrastructure.Email;
using CustomCADs.Shared.Infrastructure.Payment;
using CustomCADs.Shared.Infrastructure.Storage;
using Microsoft.Extensions.Options;
using Stripe;

#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static void AddPaymentService(this IServiceCollection services)
    {
        services.AddScoped<PaymentIntentService>();
        services.AddScoped<IPaymentService, StripeService>();
    }

    public static void AddEmailService(this IServiceCollection services)
    {
        services.AddScoped<IEmailService, MailKitService>();
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
}
