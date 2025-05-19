using Refit;

namespace CustomCADs.Shared.Speedy.API.Endpoints.CalculationEndpoints;

using Calculation;

public interface ICalculationEndpoints
{
	[Post("/")]
	Task<CalculationResponse> Calculation(CalculationRequest request, CancellationToken ct = default);
}
