using CustomCADs.Speedy.Core.Services.Models;

namespace CustomCADs.Speedy.Core.Contracts.Calculation;

internal interface ICalculationService
{
	Task<CalculateModel[]> CalculateAsync(
		AccountModel account,
		Payer payer,
		double[] weights,
		string country,
		string site,
		string street,
		CancellationToken ct = default
	);
}
