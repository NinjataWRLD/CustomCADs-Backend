using CustomCADs.Speedy.API.Endpoints.ValidationEndpoints;
using CustomCADs.Speedy.Services.Models;
using CustomCADs.Speedy.Services.Models.Shipment.Content;
using CustomCADs.Speedy.Services.Models.Shipment.Payment;
using CustomCADs.Speedy.Services.Models.Shipment.Recipient;
using CustomCADs.Speedy.Services.Models.Shipment.Sender;
using CustomCADs.Speedy.Services.Models.Shipment.Service;
using CustomCADs.Speedy.Services.Shipment.Models;

namespace CustomCADs.Speedy.Services.Validation;

public class ValidationService(IValidationEndpoints endpoints)
{
	public async Task<bool> ValidateAddress(
		AccountModel account,
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
		AccountModel account,
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
		AccountModel account,
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
		AccountModel account,
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
