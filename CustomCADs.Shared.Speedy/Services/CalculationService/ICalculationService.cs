using Refit;

namespace CustomCADs.Shared.Speedy.Services.CalculationService;

using Calculation;

public interface ICalculationService
{
    [Post("calculate")]
    Task<CalculationResponse> Calculation(CalculationRequest request, CancellationToken ct = default);
}
