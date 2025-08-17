using CustomCADs.Speedy.Http.Endpoints.ValidationEndpoints;
using CustomCADs.Speedy.Core.Services.Models;
using CustomCADs.Speedy.Core.Services.Models.Shipment.Content;
using CustomCADs.Speedy.Core.Services.Models.Shipment.Payment;
using CustomCADs.Speedy.Core.Services.Models.Shipment.Recipient;
using CustomCADs.Speedy.Core.Services.Models.Shipment.Sender;
using CustomCADs.Speedy.Core.Services.Models.Shipment.Service;
using CustomCADs.Speedy.Core.Services.Shipment.Models;
using CustomCADs.Speedy.Core.Contracts.Validation;

namespace CustomCADs.Speedy.Core.Services.Validation;


internal class ValidationService(IValidationEndpoints endpoints) : IValidationService
{
	public async Task<bool> ValidateAddress(
		SpeedyAccount account,
		ShipmentAddressModel address,
		CancellationToken ct = default)
	{
		ValidationResponse response = await endpoints.ValidateAddress(new(
			UserName: account.Username,
			Password: account.Password,
			Language: account.Language,
			ClientSystemId: account.ClientSystemId,
			Address: address.ToDto()
		), ct).ConfigureAwait(false);

		response.Error.EnsureNull();
		return response.Valid ?? false;
	}

	public async Task<bool> ValidatePostCode(
		SpeedyAccount account,
		string postCode,
		long? siteId = null,
		CancellationToken ct = default)
	{
		ValidationResponse response = await endpoints.ValidatePostCode(new(
			UserName: account.Username,
			Password: account.Password,
			Language: account.Language,
			ClientSystemId: account.ClientSystemId,
			PostCode: postCode,
			SiteId: siteId,
			CountryId: null
		), ct).ConfigureAwait(false);

		response.Error.EnsureNull();
		return response.Valid ?? false;
	}

	public async Task<bool> ValidatePhone(
		SpeedyAccount account,
		PhoneNumberModel phoneNumber,
		CancellationToken ct = default)
	{
		ValidationResponse response = await endpoints.ValidatePhone(new(
			UserName: account.Username,
			Password: account.Password,
			Language: account.Language,
			ClientSystemId: account.ClientSystemId,
			Number: phoneNumber.Number,
			Ext: phoneNumber.Extension
		), ct).ConfigureAwait(false);

		response.Error.EnsureNull();
		return response.Valid ?? false;
	}

	public async Task<bool> ValidateShipment(
		SpeedyAccount account,
		WriteShipmentModel shipment,
		CancellationToken ct = default)
	{
		ValidationResponse response = await endpoints.ValidateShipment(new(
			UserName: account.Username,
			Password: account.Password,
			Language: account.Language,
			ClientSystemId: account.ClientSystemId,
			Recipient: shipment.Recipient.ToDto(),
			Service: shipment.Service.ToDto(),
			Content: shipment.Content.ToDto(),
			Payment: shipment.Payment.ToDto(),
			Sender: shipment.Sender?.ToDto(),
			Id: shipment.Id,
			ShipmentNote: shipment.ShipmentNote,
			Ref1: shipment.Ref1,
			Ref2: shipment.Ref2,
			ConsolidationRef: shipment.ConsolidationRef,
			RequireUnsuccessfulDeliveryStickerImage: shipment.RequireUnsuccessfulDeliveryStickerImage
		), ct).ConfigureAwait(false);

		response.Error.EnsureNull();
		return response.Valid ?? false;
	}
}
