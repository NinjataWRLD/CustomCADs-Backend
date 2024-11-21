using CustomCADs.Shared.Speedy;
using CustomCADs.Shared.Speedy.API.Endpoints.ValidationEndpoints;
using CustomCADs.Shared.Speedy.Models;
using CustomCADs.Shared.Speedy.Models.Shipment;
using CustomCADs.Shared.Speedy.Models.Shipment.Content;
using CustomCADs.Shared.Speedy.Models.Shipment.Payment;
using CustomCADs.Shared.Speedy.Models.Shipment.Recipient;
using CustomCADs.Shared.Speedy.Models.Shipment.Sender;
using CustomCADs.Shared.Speedy.Models.Shipment.Service;

namespace CustomCADs.Shared.Speedy.Services.Validation;

public class ValidationService(IValidationEndpoints validation)
{
    public async Task<bool> ValidateAddress(ShipmentAddressModel address, AccountModel account, CancellationToken ct = default)
    {
        ValidationResponse response = await validation.ValidateAddress(new(
            UserName: account.Username,
            Password: account.Password,
            Language: account.Language,
            ClientSystemId: account.ClientSystemId,
            Address: address.ToDto()
        ), ct).ConfigureAwait(false);

        response.Error.EnsureNull();
        return response.Valid ?? false;
    }

    public async Task<bool> ValidatePostCode(string postCode, int countryId, AccountModel account, CancellationToken ct = default)
    {
        ValidationResponse response = await validation.ValidatePostCode(new(
            UserName: account.Username,
            Password: account.Password,
            Language: account.Language,
            ClientSystemId: account.ClientSystemId,
            PostCode: postCode,
            CountryId: countryId,
            SiteId: null
        ), ct).ConfigureAwait(false);

        response.Error.EnsureNull();
        return response.Valid ?? false;
    }
    
    public async Task<bool> ValidatePostCode(string postCode, long siteId, AccountModel account, CancellationToken ct = default)
    {
        ValidationResponse response = await validation.ValidatePostCode(new(
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

    public async Task<bool> ValidatePhone(PhoneNumberModel phoneNumber, AccountModel account, CancellationToken ct = default)
    {
        ValidationResponse response = await validation.ValidatePhone(new(
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

    public async Task<bool> ValidateShipment(ShipmentModel shipment, AccountModel account, CancellationToken ct = default)
    {
        ValidationResponse response = await validation.ValidateShipment(new(
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
