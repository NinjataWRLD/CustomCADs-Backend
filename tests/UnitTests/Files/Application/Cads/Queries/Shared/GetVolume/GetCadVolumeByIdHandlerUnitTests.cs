using CustomCADs.Files.Application.Cads.Queries.Shared;
using CustomCADs.Files.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Exceptions;
using CustomCADs.Shared.Application.UseCases.Cads.Queries;

namespace CustomCADs.UnitTests.Files.Application.Cads.Queries.Shared.GetVolume;

using static CadsData;

public class GetCadVolumeByIdHandlerUnitTests : CadsBaseUnitTests
{
	private readonly GetCadVolumeByIdHandler handler;
	private readonly Mock<ICadReads> reads = new();

	private static readonly Cad cad = CreateCad();

	public GetCadVolumeByIdHandlerUnitTests()
	{
		handler = new(reads.Object);
		reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct))
			.ReturnsAsync(cad);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		GetCadVolumeByIdQuery query = new(ValidId);

		// Act
		await handler.Handle(query, ct);

		// Assert
		reads.Verify(x => x.SingleByIdAsync(ValidId, false, ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange
		GetCadVolumeByIdQuery query = new(ValidId);

		// Act
		decimal volume = await handler.Handle(query, ct);

		// Assert
		Assert.Equal(cad.Volume, volume);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenCadNotFound()
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct))
			.ReturnsAsync(null as Cad);
		GetCadVolumeByIdQuery query = new(ValidId);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Cad>>(
			// Act
			async () => await handler.Handle(query, ct)
		);
	}
}
