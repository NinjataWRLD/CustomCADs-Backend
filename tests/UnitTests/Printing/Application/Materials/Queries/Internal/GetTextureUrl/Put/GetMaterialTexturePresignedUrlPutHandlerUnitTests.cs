using CustomCADs.Printing.Application.Materials.Queries.Internal.GetTextureUrl.Put;
using CustomCADs.Printing.Domain.Materials;
using CustomCADs.Printing.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Cache;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.Dtos.Files;
using CustomCADs.Shared.Application.UseCases.Images.Queries;

namespace CustomCADs.UnitTests.Printing.Application.Materials.Queries.Internal.GetTextureUrl.Put;

using static MaterialsData;

public class GetMaterialTexturePresignedUrlPutHandlerUnitTests : MaterialsBaseUnitTests
{
	private readonly GetMaterialTexturePresignedUrlPutHandler handler;
	private readonly Mock<IMaterialReads> reads = new();
	private readonly Mock<BaseCachingService<MaterialId, Material>> cache = new();
	private readonly Mock<IRequestSender> sender = new();

	private const string PresignedUrl = "presigned-url";
	private static readonly UploadFileRequest uploadReq = new("content-type", "file-name");

	public GetMaterialTexturePresignedUrlPutHandlerUnitTests()
	{
		handler = new(reads.Object, cache.Object, sender.Object);

		cache.Setup(x => x.GetOrCreateAsync(ValidId, It.IsAny<Func<Task<Material>>>()))
			.ReturnsAsync(CreateMaterial());

		sender.Setup(x => x.SendQueryAsync(
			It.Is<GetImagePresignedUrlPutByIdQuery>(x => x.Id == ValidTextureId && x.NewFile == uploadReq),
			ct
		)).ReturnsAsync(PresignedUrl);
	}

	[Fact]
	public async Task Handle_ShouldReadCache()
	{
		// Arrange
		GetMaterialTexturePresignedUrlPutQuery query = new(
			ValidId,
			uploadReq
		);

		// Act
		await handler.Handle(query, ct);

		// Assert
		cache.Verify(x => x.GetOrCreateAsync(ValidId, It.IsAny<Func<Task<Material>>>()), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldSendRequests()
	{
		// Arrange
		GetMaterialTexturePresignedUrlPutQuery query = new(
			ValidId,
			uploadReq
		);

		// Act
		await handler.Handle(query, ct);

		// Assert
		sender.Verify(x => x.SendQueryAsync(
			It.Is<GetImagePresignedUrlPutByIdQuery>(x => x.Id == ValidTextureId && x.NewFile == uploadReq),
			ct
		), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange
		GetMaterialTexturePresignedUrlPutQuery query = new(
			ValidId,
			uploadReq
		);

		// Act
		GetMaterialTexturePresignedUrlPutDto res = await handler.Handle(query, ct);

		// Assert
		Assert.Equal(PresignedUrl, res.PresignedUrl);
	}
}
