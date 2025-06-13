namespace CustomCADs.UnitTests.Files.Domain.Cads;

using static CadsData;

public class CadsBaseUnitTests
{
	protected static Cad CreateCad(
		string key = ValidKey,
		string contentType = ValidContentType,
		decimal volume = ValidVolume,
		decimal x1 = MinValidCoord,
		decimal y1 = MinValidCoord,
		decimal z1 = MinValidCoord,
		decimal x2 = MaxValidCoord,
		decimal y2 = MaxValidCoord,
		decimal z2 = MaxValidCoord
	)
		=> Cad.Create(
			key: key,
			contentType: contentType,
			volume: volume,
			camCoordinates: new(x1, y1, z1),
			panCoordinates: new(x2, y2, z2)
		);
}
