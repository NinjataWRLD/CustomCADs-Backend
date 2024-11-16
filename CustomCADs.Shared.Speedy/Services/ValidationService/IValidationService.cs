using Refit;

namespace CustomCADs.Shared.Speedy.Services.ValidationService;

using ValidateAddress;
using ValidatePhone;
using ValidatePostCode;
using ValidateShipment;

public interface IValidationService
{
    [Post("address")]
    Task<ValidationResponse> ValidateAddress(ValidateAddressRequest request, CancellationToken ct = default);

    [Post("postcode")]
    Task<ValidationResponse> ValidatePostCode(ValidatePostCodeRequest request, CancellationToken ct = default);

    [Post("phone")]
    Task<ValidationResponse> ValidatePhone(ValidatePhoneRequest request, CancellationToken ct = default);

    [Post("shipment")]
    Task<ValidationResponse> ValidateShipment(ValidateShipmentRequest request, CancellationToken ct = default);

}
