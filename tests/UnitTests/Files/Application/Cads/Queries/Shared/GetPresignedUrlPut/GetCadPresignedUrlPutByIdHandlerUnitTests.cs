using CustomCADs.Files.Application.Cads.Queries.Shared;
using CustomCADs.Files.Application.Cads.Storage;
using CustomCADs.Files.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Dtos.Files;
using CustomCADs.Shared.Application.UseCases.Cads.Queries;

namespace CustomCADs.UnitTests.Files.Application.Cads.Queries.Shared.GetPresignedUrlPut;

using static CadsData;

public class GetCadPresignedUrlPutByIdHandlerUnitTests : CadsBaseUnitTests
{
	private readonly GetCadPresignedUrlPutByIdHandler handler;
	private readonly Mock<ICadReads> reads = new();
	private readonly Mock<ICadStorageService> storage = new();
	private readonly Mock<BaseCachingService<CadId, Cad>> cache = new();

	private const string GeneratedKey = "generated-key";
	private const string PresignedUrl = "presigned-url";
	public static readonly UploadFileRequest req = new("content-type", "file-name");
	private static readonly Cad cad = CreateCad(key: GeneratedKey);

	public GetCadPresignedUrlPutByIdHandlerUnitTests()
	{
		handler = new(reads.Object, storage.Object, cache.Object);

		cache.Setup(x => x.GetOrCreateAsync(
			ValidId,
			It.IsAny<Func<Task<Cad>>>()
		)).ReturnsAsync(cad);

		storage.Setup(x => x.GetPresignedPutUrlAsync(GeneratedKey, req))
			.ReturnsAsync(PresignedUrl);
	}

	[Fact]
	public async Task Handle_ShouldReadCache()
	{
		// Arrange
		GetCadPresignedUrlPutByIdQuery query = new(ValidId, req);

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
		GetCadPresignedUrlPutByIdQuery query = new(ValidId, req);

		// Act
		await handler.Handle(query, ct);

		// Assert
		storage.Verify(x => x.GetPresignedPutUrlAsync(
			GeneratedKey,
			req
		), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange
		GetCadPresignedUrlPutByIdQuery query = new(ValidId, req);

		// Act
		string url = await handler.Handle(query, ct);

		// Assertres.
		Assert.Equal(PresignedUrl, url);
	}
}
