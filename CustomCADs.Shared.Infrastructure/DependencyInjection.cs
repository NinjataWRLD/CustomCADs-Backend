using CustomCADs.Shared.Core.Email;
using CustomCADs.Shared.Core.Payment;
using CustomCADs.Shared.Infrastructure.Email;
using CustomCADs.Shared.Infrastructure.Payment;
using Stripe;

#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static void AddPaymentServices(this IServiceCollection services)
    {
        services.AddScoped<PaymentIntentService>();
        services.AddScoped<IPaymentService, StripeService>();
    }

    public static void AddEmailServices(this IServiceCollection services)
    {
        services.AddScoped<IEmailService, MailKitService>();
    }
}
