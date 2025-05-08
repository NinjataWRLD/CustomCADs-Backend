namespace CustomCADs.UnitTests.Delivery.Application.Shipments.Commands.Shared.Create.Data;

using static ShipmentsData;

public class CreateShipmentInvalidRecipientData : CreateShipmentData
{
    public CreateShipmentInvalidRecipientData()
    {
        Add(ValidService1, ValidCount1, ValidWeight1, InvalidRecipient, ValidCountry1, ValidCity1, ValidStreet1, ValidPhone1, ValidEmail1);
    }
}
