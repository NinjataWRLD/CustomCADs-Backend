using Refit;

namespace CustomCADs.Speedy.Http.Endpoints.CalculationEndpoints;

using Calculation;

internal interface ICalculationEndpoints
{
	[Post("/")]
	Task<CalculationResponse> Calculation(CalculationRequest request, CancellationToken ct = default);
}
