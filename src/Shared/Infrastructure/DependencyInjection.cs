using CustomCADs.Shared.Abstractions.Cache;
using CustomCADs.Shared.Abstractions.Email;
using CustomCADs.Shared.Abstractions.Events;
using CustomCADs.Shared.Abstractions.Payment;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.Infrastructure.Cache;
using CustomCADs.Shared.Infrastructure.Email;
using CustomCADs.Shared.Infrastructure.Events;
using CustomCADs.Shared.Infrastructure.Payment;
using CustomCADs.Shared.Infrastructure.Requests;
using FluentValidation;
using JasperFx.CodeGeneration;
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
}
