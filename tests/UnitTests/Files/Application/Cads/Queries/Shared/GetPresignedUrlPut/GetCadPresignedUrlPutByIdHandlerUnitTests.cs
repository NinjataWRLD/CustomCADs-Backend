using CustomCADs.Files.Application.Cads.Queries.Shared;
using CustomCADs.Files.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Storage;
using CustomCADs.Shared.Core.Common.Dtos;
using CustomCADs.Shared.Core.Common.Exceptions.Application;
using CustomCADs.Shared.UseCases.Cads.Queries;

namespace CustomCADs.UnitTests.Files.Application.Cads.Queries.Shared.GetPresignedUrlPut;

public class GetCadPresignedUrlPutByIdHandlerUnitTests : CadsBaseUnitTests
{
	private readonly GetCadPresignedUrlPutByIdHandler handler;
	private readonly Mock<ICadReads> reads = new();
	private readonly Mock<IStorageService> storage = new();

	private const string GeneratedKey = "generated-key";
	private const string PresignedUrl = "presigned-url";
	public static readonly UploadFileRequest req = new("content-type", "file-name");
	private static readonly Cad cad = CreateCad(key: GeneratedKey);

	public GetCadPresignedUrlPutByIdHandlerUnitTests()
	{
		handler = new(reads.Object, storage.Object);

		reads.Setup(x => x.SingleByIdAsync(id, false, ct))
			.ReturnsAsync(cad);

		storage.Setup(x => x.GetPresignedPutUrlAsync(GeneratedKey, req))
			.ReturnsAsync(PresignedUrl);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		GetCadPresignedUrlPutByIdQuery query = new(id, req);

		// Act
		await handler.Handle(query, ct);

		// Assert
		reads.Verify(x => x.SingleByIdAsync(id, false, ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldCallStorage()
	{
		// Arrange
		GetCadPresignedUrlPutByIdQuery query = new(id, req);

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
		GetCadPresignedUrlPutByIdQuery query = new(id, req);

		// Act
		string url = await handler.Handle(query, ct);

		// Assertres.
		Assert.Equal(PresignedUrl, url);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenCadNotFound()
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(id, false, ct))
			.ReturnsAsync(null as Cad);
		GetCadPresignedUrlPutByIdQuery query = new(id, req);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Cad>>(
			// Act
			async () => await handler.Handle(query, ct)
		);
	}
}
