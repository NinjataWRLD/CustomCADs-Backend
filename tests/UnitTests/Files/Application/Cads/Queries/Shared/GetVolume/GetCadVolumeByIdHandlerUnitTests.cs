using CustomCADs.Files.Application.Cads.Queries.Shared;
using CustomCADs.Files.Domain.Repositories.Reads;
using CustomCADs.Shared.Core.Common.Exceptions.Application;
using CustomCADs.Shared.UseCases.Cads.Queries;

namespace CustomCADs.UnitTests.Files.Application.Cads.Queries.Shared.GetVolume;

public class GetCadVolumeByIdHandlerUnitTests : CadsBaseUnitTests
{
	private readonly Mock<ICadReads> reads = new();
	private static readonly Cad cad = CreateCad();

	public GetCadVolumeByIdHandlerUnitTests()
	{
		reads.Setup(x => x.SingleByIdAsync(id1, false, ct))
			.ReturnsAsync(cad);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Assert
		GetCadVolumeByIdQuery query = new(id1);
		GetCadVolumeByIdHandler handler = new(reads.Object);

		// Act
		await handler.Handle(query, ct);

		// Assert
		reads.Verify(x => x.SingleByIdAsync(id1, false, ct), Times.Once);
	}

	[Fact]
	public async Task Handle_ShouldReturnProperly()
	{
		// Assert
		GetCadVolumeByIdQuery query = new(id1);
		GetCadVolumeByIdHandler handler = new(reads.Object);

		// Act
		decimal volume = await handler.Handle(query, ct);

		// Assert
		Assert.Equal(cad.Volume, volume);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenCadNotFound()
	{
		// Assert
		reads.Setup(x => x.SingleByIdAsync(id1, false, ct))
			.ReturnsAsync(null as Cad);

		GetCadVolumeByIdQuery query = new(id1);
		GetCadVolumeByIdHandler handler = new(reads.Object);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Cad>>(async () =>
		{
			// Act
			await handler.Handle(query, ct);
		});
	}
}
