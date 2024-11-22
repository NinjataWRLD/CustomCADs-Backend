using CustomCADs.Shared.Speedy.API.Endpoints.ServicesEndpoints;
using CustomCADs.Shared.Speedy.Models;
using CustomCADs.Shared.Speedy.Models.Calculation;
using CustomCADs.Shared.Speedy.Models.Calculation.Recipient;
using CustomCADs.Shared.Speedy.Models.Calculation.Sender;
using CustomCADs.Shared.Speedy.Services.Services.Models;

namespace CustomCADs.Shared.Speedy.Services.Services;

using static Constants;

public class ServicesService(IServicesEndpoints endpoints)
{
    public async Task<CourierServiceModel[]> Services(DateOnly date, AccountModel account, CancellationToken ct = default)
    {
        var response = await endpoints.Services(new(
            UserName: account.Username,
            Password: account.Password,
            Language: account.Language,
            ClientSystemId: account.ClientSystemId,
            Date: date.ToString(DateFormat)
        ), ct).ConfigureAwait(false);

        response.Error.EnsureNull();
        return [.. response.Services.Select(s => s.ToModel())];
    }

    public async Task<ExtendedCourierServiceModel[]> DestinationServices(DateOnly date, CalculationRecipientModel recipient, CalculationSenderModel? sender, AccountModel account, CancellationToken ct = default)
    {
        var response = await endpoints.DestinationServices(new(
            UserName: account.Username,
            Password: account.Password,
            Language: account.Language,
            ClientSystemId: account.ClientSystemId,
            Date: date.ToString(DateFormat),
            Recipient: recipient.ToDto(),
            Sender: sender?.ToDto()
        ), ct).ConfigureAwait(false);

        response.Error.EnsureNull();
        return [.. response.Services.Select(s => s.ToModel())];
    }
}
