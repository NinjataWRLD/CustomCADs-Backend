namespace CustomCADs.UnitTests.Delivery.Application.Shipments.Queries.Shared.Data;

public class CalculateShipmentInvalidData : TheoryData<string, string, string>
{
	public CalculateShipmentInvalidData()
	{
		// Country
		Add(null!, "Burgas", "Slivnitsa");
		Add(string.Empty, "Burgas", "Slivnitsa");

		// City
		Add("Bulgaria", null!, "Slivnitsa");
		Add("Bulgaria", string.Empty, "Slivnitsa");

		// Street
		Add("Bulgaria", "Burgas", null!);
		Add("Bulgaria", "Burgas", string.Empty);
	}
}
