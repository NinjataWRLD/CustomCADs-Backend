namespace CustomCADs.UnitTests.Printing.Domain.Customizations.Create.Data;

using static CustomizationsData;

public class CustomizationCreateInvalidData : TheoryData<decimal, decimal, decimal, string>
{
	public CustomizationCreateInvalidData()
	{
		// Scale
		Add(MaxInvalidScale, MaxValidInfill, MaxValidVolume, ValidColor);
		Add(MinInvalidScale, MinValidInfill, MinValidVolume, ValidColor);

		// Infill
		Add(MaxValidScale, MaxInvalidInfill, MaxValidVolume, ValidColor);
		Add(MinValidScale, MinInvalidInfill, MinValidVolume, ValidColor);

		// Volume
		Add(MaxValidScale, MaxValidInfill, MaxInvalidVolume, ValidColor);
		Add(MinValidScale, MinValidInfill, MinInvalidVolume, ValidColor);

		// Color
		Add(MaxValidScale, MaxValidInfill, MaxValidVolume, InvalidColor);
		Add(MinValidScale, MinValidInfill, MinValidVolume, MaxInvalidColor);
		Add(MaxValidScale, MaxValidInfill, MaxValidVolume, MinInvalidColor);
	}
}
