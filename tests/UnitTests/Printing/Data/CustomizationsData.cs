using CustomCADs.Printing.Domain.Customizations;
using CustomCADs.Shared.Domain.TypedIds.Printing;

namespace CustomCADs.UnitTests.Printing.Data;

using static CustomizationConstants;

public static class CustomizationsData
{
	public const string ValidColor = "#ffffff";
	public const string InvalidColor = "";
	public const string MinInvalidColor = "#";
	public const string MaxInvalidColor = "#ffffffffffff";

	public const decimal MaxValidScale = ScaleMax - 1;
	public const decimal MinValidScale = ScaleMin + 1;
	public const decimal MaxInvalidScale = ScaleMax + 1;
	public const decimal MinInvalidScale = ScaleMin - 1;

	public const decimal MaxValidInfill = InfillMax - 1m / 100;
	public const decimal MinValidInfill = InfillMin + 1m / 100;
	public const decimal MaxInvalidInfill = InfillMax + 1m / 100;
	public const decimal MinInvalidInfill = InfillMin - 1m / 100;

	public const decimal MinValidVolume = VolumeMin + 1;
	public const decimal MaxValidVolume = VolumeMax - 1;
	public const decimal MinInvalidVolume = VolumeMin - 1;
	public const decimal MaxInvalidVolume = VolumeMax + 1;

	public static readonly CustomizationId ValidId = CustomizationId.New();
	public static readonly MaterialId ValidMaterialId = MaterialId.New();
}
