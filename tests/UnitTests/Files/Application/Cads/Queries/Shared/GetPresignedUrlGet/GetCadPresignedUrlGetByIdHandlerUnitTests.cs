using CustomCADs.Files.Application.Cads.Queries.Shared;
using CustomCADs.Files.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Storage;
using CustomCADs.Shared.Core.Common.Exceptions.Application;
using CustomCADs.Shared.UseCases.Cads.Queries;

namespace CustomCADs.UnitTests.Files.Application.Cads.Queries.Shared.GetPresignedUrlGet;

public class GetCadPresignedUrlGetByIdHandlerUnitTests : CadsBaseUnitTests
{
	private readonly Mock<ICadReads> reads = new();
	private readonly Mock<IStorageService> storage = new();
	private static readonly Cad cad = CreateCad();
	public const string PresignedUrl = "Url";

	public GetCadPresignedUrlGetByIdHandlerUnitTests()
	{
		reads.Setup(x => x.SingleByIdAsync(id1, false, ct))
			.ReturnsAsync(cad);

		storage.Setup(x => x.GetPresignedGetUrlAsync(cad.Key, cad.ContentType))
			.ReturnsAsync(PresignedUrl);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Assert
		GetCadPresignedUrlGetByIdQuery query = new(id1);
		GetCadPresignedUrlGetByIdHandler handler = new(reads.Object, storage.Object);

		// Act
		await handler.Handle(query, ct);

		// Assert
		reads.Verify(x => x.SingleByIdAsync(id1, false, ct), Times.Once);
	}

	[Fact]
	public async Task Handle_ShouldCallStorage()
	{
		// Assert
		GetCadPresignedUrlGetByIdQuery query = new(id1);
		GetCadPresignedUrlGetByIdHandler handler = new(reads.Object, storage.Object);

		// Act
		await handler.Handle(query, ct);

		// Assert
		storage.Verify(x => x.GetPresignedGetUrlAsync(
			cad.Key,
			cad.ContentType
		), Times.Once);
	}

	[Fact]
	public async Task Handle_ShouldReturnProperly()
	{
		// Assert
		GetCadPresignedUrlGetByIdQuery query = new(id1);
		GetCadPresignedUrlGetByIdHandler handler = new(reads.Object, storage.Object);

		// Act
		var (Url, ContentType) = await handler.Handle(query, ct);

		// Assert
		Assert.Multiple(
			() => Assert.Equal(cad.ContentType, ContentType),
			() => Assert.Equal(PresignedUrl, Url)
		);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenCadNotFound()
	{
		// Assert
		reads.Setup(x => x.SingleByIdAsync(id1, false, ct))
			.ReturnsAsync(null as Cad);

		GetCadPresignedUrlGetByIdQuery query = new(id1);
		GetCadPresignedUrlGetByIdHandler handler = new(reads.Object, storage.Object);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Cad>>(async () =>
		{
			// Act
			await handler.Handle(query, ct);
		});
	}
}
