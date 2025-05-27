using CustomCADs.Shared.Speedy.API.Endpoints.PickupEndpoints;
using CustomCADs.Shared.Speedy.API.Endpoints.PickupEndpoints.Enums;
using CustomCADs.Shared.Speedy.Services.Models;
using CustomCADs.Shared.Speedy.Services.Models.Calculation;
using CustomCADs.Shared.Speedy.Services.Models.Calculation.Sender;

namespace CustomCADs.Shared.Speedy.Services.Pickup;

using static Constants;

public class PickupService(IPickupEndpoints endpoints)
{
	public async Task<(long Id, string[] ShipmentIds, DateTime? PickupPeriodFrom, DateTime? PickupPeriodTo)[]> Pickup(
		AccountModel account,
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
		AccountModel account,
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
