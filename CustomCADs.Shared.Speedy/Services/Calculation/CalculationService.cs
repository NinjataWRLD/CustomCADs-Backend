using CustomCADs.Shared.Speedy.API.Endpoints.CalculationEndpoints;
using CustomCADs.Shared.Speedy.Models;
using CustomCADs.Shared.Speedy.Models.Calculation;
using CustomCADs.Shared.Speedy.Models.Shipment.Payment;
using CustomCADs.Shared.Speedy.Services.Calculation.Models;

namespace CustomCADs.Shared.Speedy.Services.Calculation;

public class CalculationService(ICalculationEndpoints endpoints)
{
    public async Task<CalculationResultModel[]> Calculate(CalculationModel model, AccountModel account, CancellationToken ct = default)
    {
        var response = await endpoints.Calculation(new(
            UserName: account.Username,
            Password: account.Password,
            Location: account.Language,
            ClientSystemId: account.ClientSystemId,
            Recipient: model.Recipient.ToDto(),
            Service: model.Service.ToDto(),
            Content: model.Content.ToDto(),
            Payment: model.Payment.ToDto(),
            Sender: model.Sender?.ToDto()
        ), ct).ConfigureAwait(false);

        response.Error.EnsureNull();
        return [.. response.Calculations.Select(c => c.ToModel())];
    }
}
