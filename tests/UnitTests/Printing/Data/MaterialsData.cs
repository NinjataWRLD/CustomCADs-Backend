using CustomCADs.Printing.Domain.Materials;
using CustomCADs.Shared.Domain.TypedIds.Files;
using CustomCADs.Shared.Domain.TypedIds.Printing;

namespace CustomCADs.UnitTests.Printing.Data;

using static MaterialConstants;

public static class MaterialsData
{
	public static readonly string MaxValidName = new('a', NameMaxLength - 1);
	public static readonly string MinValidName = new('a', NameMinLength + 1);
	public static readonly string InvalidName = string.Empty;
	public static readonly string MaxInvalidName = new('a', NameMaxLength + 1);
	public static readonly string MinInvalidName = new('a', NameMinLength - 1);

	public const decimal MaxValidDensity = DensityMax - 1;
	public const decimal MinValidDensity = DensityMin + 1;
	public const decimal MaxInvalidDensity = DensityMax + 1;
	public const decimal MinInvalidDensity = DensityMin - 1;

	public const decimal MaxValidCost = CostMax - 1;
	public const decimal MinValidCost = CostMin + 1;
	public const decimal MaxInvalidCost = CostMax + 1;
	public const decimal MinInvalidCost = CostMin - 1;

	public static readonly MaterialId ValidId = MaterialId.New();
	public static readonly ImageId ValidTextureId = ImageId.New();
}

