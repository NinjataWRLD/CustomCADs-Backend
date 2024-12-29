namespace CustomCADs.UnitTests.Delivery.Application.Shipments.SharedCommands.Data;

public class CreateShipmentHandlerValidData : CreateShipmentHandlerData
{
    public CreateShipmentHandlerValidData()
    {
        Add(ShipmentValidService1, ShipmentValidCount1, ShipmentValidWeight1, ShipmentValidRecipient1, ShipmentValidCountry1, ShipmentValidCity1, ShipmentValidPhone1, ShipmentValidEmail1);
        Add(ShipmentValidService2, ShipmentValidCount2, ShipmentValidWeight2, ShipmentValidRecipient2, ShipmentValidCountry2, ShipmentValidCity2, ShipmentValidPhone2, ShipmentValidEmail2);
    }
}
