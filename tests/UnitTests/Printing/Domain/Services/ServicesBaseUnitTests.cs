using CustomCADs.Printing.Domain.Customizations;
using CustomCADs.Printing.Domain.Materials;
using CustomCADs.Shared.Core.Common.TypedIds.Files;

namespace CustomCADs.UnitTests.Printing.Domain.Services;

public class ServicesBaseUnitTests
{
	protected static Customization CreateCustomization(
		decimal? scale = null,
		decimal? infill = null,
		decimal? volume = null,
		string? color = null,
		MaterialId? materialId = null
	) => Customization.Create(
		scale: scale ?? CustomizationsData.MinValidScale,
		infill: infill ?? CustomizationsData.MinValidInfill,
		volume: volume ?? CustomizationsData.MinValidVolume,
		color: color ?? CustomizationsData.ValidColor,
		materialId: materialId ?? CustomizationsData.ValidMaterialId
	);

	protected static Material CreateMaterial(
		string? name = null,
		decimal? density = null,
		decimal? cost = null,
		ImageId? textureId = null
	) => Material.Create(
			name: name ?? MaterialsData.MinValidName,
			density: density ?? MaterialsData.MinValidDensity,
			cost: cost ?? MaterialsData.MinValidCost,
			textureId: textureId ?? MaterialsData.ValidTextureId
		);
}
