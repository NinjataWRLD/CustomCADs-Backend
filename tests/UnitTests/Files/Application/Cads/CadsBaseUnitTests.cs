using CustomCADs.Files.Domain.Cads.ValueObjects;

namespace CustomCADs.UnitTests.Files.Application.Cads;

using static CadsData;

public class CadsBaseUnitTests
{
	protected static readonly CancellationToken ct = CancellationToken.None;

	protected static Cad CreateCad(string? key = null, string? contentType = null, decimal? volume = null, Coordinates? cam = null, Coordinates? pan = null)
		=> Cad.Create(
			key: key ?? ValidKey,
			contentType: contentType ?? ValidContentType,
			volume: volume ?? ValidVolume,
			camCoordinates: cam ?? new(MinValidCoord, MinValidCoord, MinValidCoord),
			panCoordinates: pan ?? new(MinValidCoord, MinValidCoord, MinValidCoord)
		);

	protected static Cad CreateCadWithId(CadId? id = null, string? key = null, string? contentType = null, decimal? volume = null, Coordinates? cam = null, Coordinates? pan = null)
		=> Cad.CreateWithId(
			id: id ?? ValidId,
			key: key ?? ValidKey,
			contentType: contentType ?? ValidContentType,
			volume: volume ?? ValidVolume,
			camCoordinates: cam ?? new(MinValidCoord, MinValidCoord, MinValidCoord),
			panCoordinates: pan ?? new(MinValidCoord, MinValidCoord, MinValidCoord)
		);
}
