#pragma warning disable IDE0130
using CustomCADs.Shared.Application.Email;
using CustomCADs.Shared.Application.Payment;
using CustomCADs.Shared.Infrastructure.Email;
using CustomCADs.Shared.Infrastructure.Payment;
using Stripe;

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
