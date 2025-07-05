using CustomCADs.Customizations.Domain.Customizations;
using CustomCADs.Customizations.Domain.Materials;
using CustomCADs.Shared.Core.Common.TypedIds.Files;

namespace CustomCADs.UnitTests.Customizations.Application.Customizations;

using static CustomizationsData;

public class CustomizationsBaseUnitTests
{
	protected static readonly CancellationToken ct = default;

	protected static Customization CreateCustomization(
		decimal? scale = null,
		decimal? infill = null,
		decimal? volume = null,
		string? color = null,
		MaterialId? materialId = null
	) => Customization.Create(
		scale: scale ?? MinValidScale,
		infill: infill ?? MinValidInfill,
		volume: volume ?? MinValidVolume,
		color: color ?? ValidColor,
		materialId: materialId ?? ValidMaterialId
	);

	protected static Customization CreateCustomizationWithId(
		CustomizationId? id = null,
		decimal? scale = null,
		decimal? infill = null,
		decimal? volume = null,
		string? color = null,
		MaterialId? materialId = null
	) => Customization.CreateWithId(
		id: id ?? ValidId,
		scale: scale ?? MinValidScale,
		infill: infill ?? MinValidInfill,
		volume: volume ?? MinValidVolume,
		color: color ?? ValidColor,
		materialId: materialId ?? ValidMaterialId
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
