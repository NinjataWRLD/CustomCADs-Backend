using CustomCADs.Files.Domain.Cads;
using CustomCADs.Shared.Domain.TypedIds.Files;

namespace CustomCADs.UnitTests.Files.Data;

using static CadConstants.Coordinates;

public class CadsData
{
	public const string ValidKey = "key-to-cad";
	public const string InvalidKey = "";

	public const string ValidContentType = "model/gltf+json";
	public const string InvalidContentType = "";

	public const decimal ValidVolume = 1000;
	public const decimal InvalidVolume = 0;

	public const decimal MinValidCoord = CoordMin + 1;
	public const decimal MaxValidCoord = CoordMax - 1;
	public const decimal MinInvalidCoord = CoordMin - 1;
	public const decimal MaxInvalidCoord = CoordMax + 1;

	public static readonly CadId ValidId = CadId.New();
}
