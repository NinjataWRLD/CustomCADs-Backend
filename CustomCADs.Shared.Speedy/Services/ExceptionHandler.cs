namespace CustomCADs.Shared.Speedy.Services;

public static class ExceptionHandler
{
    public static void EnsureNull(this ErrorDto? error)
    {
        if (error is null) return;

        throw error.Code switch
        {
            100 => new SpeedyInvalidClientDataException(error.Message),
            120 => new SpeedyInvalidAddressException(error.Message),
            160 => new SpeedyInvalidDropOffOfficeException(error.Message),
            180 => new SpeedyInvalidPickupOfficeException(error.Message),
            300 => new SpeedyInvalidThirdPartyException(error.Message),
            400 => new SpeedyInvalidCourierServiceException(error.Message),
            410 => new SpeedyInvalidCodAdditionalServiceException(error.Message),
            415 => new SpeedyInvalidDeclaredValueAdditionalServiceException(error.Message),
            420 => new SpeedyInvalidObpdAdditionalServiceException(error.Message),
            425 => new SpeedyInvalidDeliveryToFloorAdditionalServiceException(error.Message),
            430 => new SpeedyInvalidSpecialDeliveryAdditionalServiceException(error.Message),
            435 => new SpeedyInvalidRodAdditionalServiceException(error.Message),
            440 => new SpeedyInvalidReturnReceiptAdditionalServiceException(error.Message),
            445 => new SpeedyInvalidSwapAdditionalServiceException(error.Message),
            450 => new SpeedyInvalidRopAdditionalServiceException(error.Message),
            455 => new SpeedyInvalidReturnVoucherAdditionalServiceException(error.Message),
            460 => new SpeedyInvalidFixedTimeDeliveryAdditionalServiceException(error.Message),
            500 => new SpeedyInvalidPickupDateException(error.Message),
            510 => new SpeedyInvalidDeferredDaysException(error.Message),
            515 => new SpeedyInvalidDiscountCardException(error.Message),
            520 => new SpeedyInvalidSaturdayDeliveryFlagException(error.Message),
            600 => new SpeedyInvalidShipmentContentsException(error.Message),
            605 => new SpeedyInvalidShipmentPackageException(error.Message),
            610 => new SpeedyInvalidDocumentsFlagException(error.Message),
            615 => new SpeedyInvalidPalletizedFlagException(error.Message),
            620 => new SpeedyInvalidWeightException(error.Message),
            630 => new SpeedyInvalidParcelsException(error.Message),
            700 => new SpeedyInvalidCourierServicePaymentException(error.Message),
            710 => new SpeedyInvalidPackingsPaymentException(error.Message),
            800 => new SpeedyInvalidRef1Exception(error.Message),
            805 => new SpeedyInvalidRef2Exception(error.Message),
            810 => new SpeedyInvalidShipmentNoteException(error.Message),
            _ => new SpeedyGeneralException(error.Message),
        };
    }
}

/// <summary>
///     Returned when there is an error in request processing and it doesn't match any of recongizable errors described in this table
/// </summary>
/// <param name="message"></param>
public class SpeedyGeneralException(string message) : Exception(message);

/// <summary>
///     Returned when there is invalid client data provided in request. Relevant for Shipment, Calculation and Client service methods 
/// </summary>
/// <param name="message"></param>
public class SpeedyInvalidClientDataException(string message) : Exception(message);

/// <summary>
///     Returned when there is an error in address provided in request. Relevant for Shipment, Calculation and Client service methods
/// </summary>
/// <param name="message"></param>
public class SpeedyInvalidAddressException(string message) : Exception(message);

/// <summary>
///     Returned when the selected sender drop-off office in request is not valid. Relevant for Shipment and Calculation service methods
/// </summary>
/// <param name="message"></param>
public class SpeedyInvalidDropOffOfficeException(string message) : Exception(message);

/// <summary>
///     Returned when the selected recipient pickup office in request is not valid. Relevant for Shipment and Calculation service methods
/// </summary>
/// <param name="message"></param>
public class SpeedyInvalidPickupOfficeException(string message) : Exception(message);

/// <summary>
///     Returned when the selected third party payer in request is invalid. Relevant for Shipment and Calculation service methods
/// </summary>
/// <param name="message"></param>
public class SpeedyInvalidThirdPartyException(string message) : Exception(message);

/// <summary>
///     Returned when the courier service in request is invalid. Relevant for Shipment and Calculation service methods
/// </summary>
/// <param name="message"></param>
public class SpeedyInvalidCourierServiceException(string message) : Exception(message);

/// <summary>
///     Returned when COD additional service data in request is invalid. Relevant for Shipment and Calculation service methods
/// </summary>
/// <param name="message"></param>
public class SpeedyInvalidCodAdditionalServiceException(string message) : Exception(message);

/// <summary>
///     Returned when Declared Value additional service data in request is invalid.Relevant for Shipment and Calculation service methods
/// </summary>
/// <param name="message"></param>
public class SpeedyInvalidDeclaredValueAdditionalServiceException(string message) : Exception(message);

/// <summary>
///     Returned when Options Before Payment and Delivery additional service data in request is invalid. Relevant for Shipment and Calculation service methods
/// </summary>
/// <param name="message"></param>
public class SpeedyInvalidObpdAdditionalServiceException(string message) : Exception(message);

/// <summary>
///     Returned when Delivery To Floor additional service data in request is invalid. Relevant for Shipment and Calculation service methods
/// </summary>
/// <param name="message"></param>
public class SpeedyInvalidDeliveryToFloorAdditionalServiceException(string message) : Exception(message);

/// <summary>
///     Returned when Special Deliery additional service data in request is invalid. Relevant for Shipment and Calculation service methods
/// </summary>
/// <param name="message"></param>
public class SpeedyInvalidSpecialDeliveryAdditionalServiceException(string message) : Exception(message);

/// <summary>
///     Returned when Return of Documents additional service data in request is invalid. Relevant for Shipment and Calculation service methods
/// </summary>
/// <param name="message"></param>
public class SpeedyInvalidRodAdditionalServiceException(string message) : Exception(message);

/// <summary>
///     Returned when Return Receipt additional service data in request is invalid. Relevant for Shipment and Calculation service methods
/// </summary>
/// <param name="message"></param>
public class SpeedyInvalidReturnReceiptAdditionalServiceException(string message) : Exception(message);

/// <summary>
///     Returned when SWAP additional service data in request is invalid. Relevant for Shipment and Calculation service methods
/// </summary>
/// <param name="message"></param>
public class SpeedyInvalidSwapAdditionalServiceException(string message) : Exception(message);

/// <summary>
///     Returned when Return of Pallets additional service data in request is invalid. Relevant for Shipment and Calculation service methods
/// </summary>
/// <param name="message"></param>
public class SpeedyInvalidRopAdditionalServiceException(string message) : Exception(message);

/// <summary>
///     Returned when Return Voucher additional service data in request is invalid. Relevant for Shipment and Calculation service methods
/// </summary>
/// <param name="message"></param>
public class SpeedyInvalidReturnVoucherAdditionalServiceException(string message) : Exception(message);

/// <summary>
///     Returned when Fixed Time Delivery additional service data in request is invalid. Relevant for Shipment and Calculation service methods
/// </summary>
/// <param name="message"></param>
public class SpeedyInvalidFixedTimeDeliveryAdditionalServiceException(string message) : Exception(message);

/// <summary>
///     Returned when pickup date is not valid. Relevant for Shipment and Calculation service methods
/// </summary>
/// <param name="message"></param>
public class SpeedyInvalidPickupDateException(string message) : Exception(message);

/// <summary>
///     Returned when deferred days provided in request is not valid. Relevant for Shipment and Calculation service methods
/// </summary>
/// <param name="message"></param>
public class SpeedyInvalidDeferredDaysException(string message) : Exception(message);

/// <summary>
///     Returned when discount card provided in request is not valid. Relevant for Shipment service methods
/// </summary>
/// <param name="message"></param>
public class SpeedyInvalidDiscountCardException(string message) : Exception(message);

/// <summary>
///     Returned when saturday delivery flag provided in request is not valid. Relevant for Shipment and Calculation service methods
/// </summary>
/// <param name="message"></param>
public class SpeedyInvalidSaturdayDeliveryFlagException(string message) : Exception(message);

/// <summary>
///     Returned when shipment contents provided in request is not valid. Relevant for Shipment service methods
/// </summary>
/// <param name="message"></param>
public class SpeedyInvalidShipmentContentsException(string message) : Exception(message);

/// <summary>
///     Returned when shipment package provided in request is not valid. Relevant for Shipment service methods
/// </summary>
/// <param name="message"></param>
public class SpeedyInvalidShipmentPackageException(string message) : Exception(message);

/// <summary>
///     Returned when documents flag provided in request is not valid. Relevant for Shipment and Calculation service methods
/// </summary>
/// <param name="message"></param>
public class SpeedyInvalidDocumentsFlagException(string message) : Exception(message);

/// <summary>
///     Returned when palletized flag provided in request is not valid. Relevant for Shipment and Calculation service methods
/// </summary>
/// <param name="message"></param>
public class SpeedyInvalidPalletizedFlagException(string message) : Exception(message);

/// <summary>
///     Returned when total weight or parcel weight in request is not valid. Relevant for Shipment and Calculation service methods
/// </summary>
/// <param name="message"></param>
public class SpeedyInvalidWeightException(string message) : Exception(message);

/// <summary>
///     Returned when there is an error in parcels data provided in request. Relevant for Shipment and Calculation service methods
/// </summary>
/// <param name="message"></param>
public class SpeedyInvalidParcelsException(string message) : Exception(message);

/// <summary>
///     Returned when there is an error in courier service payment data provided in request. Relevant for Shipment and Calculation service methods
/// </summary>
/// <param name="message"></param>
public class SpeedyInvalidCourierServicePaymentException(string message) : Exception(message);

/// <summary>
///     Returned when there is an error in packings payment data provided in request. Relevant for Shipment and Calculation service methods
/// </summary>
/// <param name="message"></param>
public class SpeedyInvalidPackingsPaymentException(string message) : Exception(message);

/// <summary>
///     Returned when shipment reference 1 provided in request. Relevant for Shipment service methods
/// </summary>
/// <param name="message"></param>
public class SpeedyInvalidRef1Exception(string message) : Exception(message);

/// <summary>
///     Returned when shipment reference 2 provided in request. Relevant for Shipment service methods
/// </summary>
/// <param name="message"></param>
public class SpeedyInvalidRef2Exception(string message) : Exception(message);

/// <summary>
///     Returned when shipment note provided in request. Relevant for Shipment service methods
/// </summary>
/// <param name="message"></param>
public class SpeedyInvalidShipmentNoteException(string message) : Exception(message);