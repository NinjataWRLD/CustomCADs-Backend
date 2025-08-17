using CustomCADs.Speedy.Http.Endpoints.PickupEndpoints;
using CustomCADs.Speedy.Core.Services.Models.Calculation;
using CustomCADs.Speedy.Core.Services.Models.Calculation.Sender;
using CustomCADs.Speedy.Core.Contracts.Pickup;

namespace CustomCADs.Speedy.Core.Services.Pickup;

using static Constants;

internal class PickupService(IPickupEndpoints endpoints) : IPickupService
{
	public async Task<PickupModel[]> Pickup(
		SpeedyAccount account,
		TimeOnly visitEndTime,
		PickupScope pickupScope = PickupScope.EXPLICIT_SHIPMENT_ID_LIST,
		DateTime? pickupDateTime = null,
		bool? autoAdjustPickupDate = null,
		string[]? explicitShipmentIdList = null,
		string? contactName = null,
		string? phoneNumber = null,
		CancellationToken ct = default
	)
	{
		var response = await endpoints.Pickup(new(
			UserName: account.Username,
			Password: account.Password,
			Language: account.Language,
			ClientSystemId: account.ClientSystemId,
			VisitEndTime: visitEndTime.ToString(DateTimeFormat),
			PickupDateTime: pickupDateTime?.ToString(DateTimeFormat),
			AutoAdjustPickupDate: autoAdjustPickupDate,
			PickupScope: pickupScope,
			ExplicitShipmentIdList: explicitShipmentIdList,
			ContactName: contactName,
			PhoneNumber: phoneNumber
		), ct).ConfigureAwait(false);

		response.Error.EnsureNull();
		return [.. response.Orders.Select(o => o.ToModel())];
	}

	public async Task<string[]> PickupTerms(
		SpeedyAccount account,
		int serviceId,
		DateOnly? startingDate = null,
		CalculationSenderModel? sender = null,
		bool senderHasPayment = false,
		CancellationToken ct = default
	)
	{
		var response = await endpoints.PickupTerms(new(
			UserName: account.Username,
			Password: account.Password,
			Language: account.Language,
			ClientSystemId: account.ClientSystemId,
			ServiceId: serviceId,
			StartingDate: startingDate?.ToString(DateFormat),
			Sender: sender?.ToDto(),
			SenderHasPayment: senderHasPayment
		), ct).ConfigureAwait(false);

		response.Error.EnsureNull();
		return response.Cutoffs;
	}
}
