using CustomCADs.Files.Application.Cads.Queries.Shared;
using CustomCADs.Files.Application.Cads.Storage;
using CustomCADs.Files.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Exceptions;
using CustomCADs.Shared.Application.UseCases.Cads.Queries;

namespace CustomCADs.UnitTests.Files.Application.Cads.Queries.Shared.GetPresignedUrlGet;

using static CadsData;

public class GetCadPresignedUrlGetByIdHandlerUnitTests : CadsBaseUnitTests
{
	private readonly GetCadPresignedUrlGetByIdHandler handler;
	private readonly Mock<ICadReads> reads = new();
	private readonly Mock<ICadStorageService> storage = new();
	private readonly Mock<BaseCachingService<CadId, Cad>> cache = new();

	public const string PresignedUrl = "Url";
	private static readonly Cad cad = CreateCad();

	public GetCadPresignedUrlGetByIdHandlerUnitTests()
	{
		handler = new(reads.Object, storage.Object, cache.Object);

		cache.Setup(x => x.GetOrCreateAsync(
			ValidId,
			It.IsAny<Func<Task<Cad>>>()
		)).ReturnsAsync(cad);

		storage.Setup(x => x.GetPresignedGetUrlAsync(cad.Key, cad.ContentType))
			.ReturnsAsync(PresignedUrl);
	}

	[Fact]
	public async Task Handle_ShouldReadCache()
	{
		// Arrange
		GetCadPresignedUrlGetByIdQuery query = new(ValidId);

		// Act
		await handler.Handle(query, ct);

		// Assert
		cache.Verify(
			x => x.GetOrCreateAsync(ValidId, It.IsAny<Func<Task<Cad>>>()),
			Times.Once()
		);
	}

	[Fact]
	public async Task Handle_ShouldCallStorage()
	{
		// Arrange
		GetCadPresignedUrlGetByIdQuery query = new(ValidId);

		// Act
		await handler.Handle(query, ct);

		// Assert
		storage.Verify(x => x.GetPresignedGetUrlAsync(
			cad.Key,
			cad.ContentType
		), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange
		GetCadPresignedUrlGetByIdQuery query = new(ValidId);

		// Act
		var (Url, ContentType) = await handler.Handle(query, ct);

		// Assert
		Assert.Multiple(
			() => Assert.Equal(cad.ContentType, ContentType),
			() => Assert.Equal(PresignedUrl, Url)
		);
	}
}
