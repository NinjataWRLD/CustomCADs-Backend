using CustomCADs.Printing.Domain.Materials;
using CustomCADs.Shared.Domain.TypedIds.Files;

namespace CustomCADs.UnitTests.Printing.Domain.Materials;

using static MaterialsData;

public class MaterialsBaseUnitTests
{
	protected static Material CreateMaterial(
		string? name = null,
		decimal? density = null,
		decimal? cost = null,
		ImageId? textureId = null
	) => Material.Create(
			name: name ?? MinValidName,
			density: density ?? MinValidDensity,
			cost: cost ?? MinValidCost,
			textureId: textureId ?? ValidTextureId
		);

	protected static Material CreateMaterialWithId(
		MaterialId? id = null,
		string? name = null,
		decimal? density = null,
		decimal? cost = null,
		ImageId? textureId = null
	) => Material.CreateWithId(
			id: id ?? ValidId,
			name: name ?? MinValidName,
			density: density ?? MinValidDensity,
			cost: cost ?? MinValidCost,
			textureId: textureId ?? ValidTextureId
		);
}
