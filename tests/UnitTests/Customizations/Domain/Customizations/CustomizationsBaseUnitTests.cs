using CustomCADs.Customizations.Domain.Customizations;

namespace CustomCADs.UnitTests.Customizations.Domain.Customizations;

using static CustomizationsData;

public class CustomizationsBaseUnitTests
{
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
}
