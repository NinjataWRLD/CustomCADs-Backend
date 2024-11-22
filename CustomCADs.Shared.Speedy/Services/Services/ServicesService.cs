using CustomCADs.Shared.Speedy.API.Endpoints.ServicesEndpoints;
using CustomCADs.Shared.Speedy.Services.Models;
using CustomCADs.Shared.Speedy.Services.Models.Calculation;
using CustomCADs.Shared.Speedy.Services.Models.Calculation.Recipient;
using CustomCADs.Shared.Speedy.Services.Models.Calculation.Sender;
using CustomCADs.Shared.Speedy.Services.Services.Models;

namespace CustomCADs.Shared.Speedy.Services.Services;

using static Constants;

public class ServicesService(IServicesEndpoints endpoints)
{
    public async Task<CourierServiceModel[]> Services(
        AccountModel account,
        DateOnly? date = null,
        CancellationToken ct = default)
    {
        var response = await endpoints.Services(new(
            UserName: account.Username,
            Password: account.Password,
            Language: account.Language,
            ClientSystemId: account.ClientSystemId,
            Date: date?.ToString(DateFormat)
        ), ct).ConfigureAwait(false);

        response.Error.EnsureNull();
        return [.. response.Services.Select(s => s.ToModel())];
    }

    public async Task<ExtendedCourierServiceModel[]> DestinationServices(
        AccountModel account,
        CalculationRecipientModel recipient,
        DateOnly? date = null,
        CalculationSenderModel? sender = null,
        CancellationToken ct = default)
    {
        var response = await endpoints.DestinationServices(new(
            UserName: account.Username,
            Password: account.Password,
            Language: account.Language,
            ClientSystemId: account.ClientSystemId,
            Recipient: recipient.ToDto(),
            Date: date?.ToString(DateFormat),
            Sender: sender?.ToDto()
        ), ct).ConfigureAwait(false);

        response.Error.EnsureNull();
        return [.. response.Services.Select(s => s.ToModel())];
    }
}
