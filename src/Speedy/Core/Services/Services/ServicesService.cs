using CustomCADs.Speedy.Http.Endpoints.ServicesEndpoints;
using CustomCADs.Speedy.Core.Services.Models;
using CustomCADs.Speedy.Core.Services.Models.Calculation;
using CustomCADs.Speedy.Core.Services.Models.Calculation.Recipient;
using CustomCADs.Speedy.Core.Services.Models.Calculation.Sender;
using CustomCADs.Speedy.Core.Services.Services.Models;

namespace CustomCADs.Speedy.Core.Services.Services;

using static Constants;

internal class ServicesService(IServicesEndpoints endpoints)
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

	public async Task<(string Deadline, CourierServiceModel CourierService)[]> DestinationServices(
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
