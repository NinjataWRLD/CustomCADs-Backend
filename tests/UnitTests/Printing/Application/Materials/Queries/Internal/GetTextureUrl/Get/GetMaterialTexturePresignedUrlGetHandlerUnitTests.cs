using CustomCADs.Printing.Application.Materials.Queries.Internal.GetTextureUrl.Get;
using CustomCADs.Printing.Domain.Materials;
using CustomCADs.Printing.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Cache;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.Dtos.Files;
using CustomCADs.Shared.Application.UseCases.Images.Queries;

namespace CustomCADs.UnitTests.Printing.Application.Materials.Queries.Internal.GetTextureUrl.Get;

using static MaterialsData;

public class GetMaterialTexturePresignedUrlGetHandlerUnitTests : MaterialsBaseUnitTests
{
	private readonly GetMaterialTexturePresignedUrlGetHandler handler;
	private readonly Mock<IMaterialReads> reads = new();
	private readonly Mock<BaseCachingService<MaterialId, Material>> cache = new();
	private readonly Mock<IRequestSender> sender = new();

	private static readonly DownloadFileResponse download = new("presigned-url", "content-type");

	public GetMaterialTexturePresignedUrlGetHandlerUnitTests()
	{
		handler = new(reads.Object, cache.Object, sender.Object);

		cache.Setup(x => x.GetOrCreateAsync(ValidId, It.IsAny<Func<Task<Material>>>()))
			.ReturnsAsync(CreateMaterial());

		sender.Setup(x => x.SendQueryAsync(
			It.Is<GetImagePresignedUrlGetByIdQuery>(x => x.Id == ValidTextureId),
			ct
		)).ReturnsAsync(download);
	}

	[Fact]
	public async Task Handle_ShouldReadCache()
	{
		// Arrange
		GetMaterialTexturePresignedUrlGetQuery query = new(ValidId);

		// Act
		await handler.Handle(query, ct);

		// Assert
		cache.Verify(x => x.GetOrCreateAsync(ValidId, It.IsAny<Func<Task<Material>>>()), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldSendRequests()
	{
		// Arrange
		GetMaterialTexturePresignedUrlGetQuery query = new(ValidId);

		// Act
		await handler.Handle(query, ct);

		// Assert
		sender.Verify(x => x.SendQueryAsync(
			It.Is<GetImagePresignedUrlGetByIdQuery>(x => x.Id == ValidTextureId),
			ct
		), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange
		GetMaterialTexturePresignedUrlGetQuery query = new(ValidId);

		// Act
		DownloadFileResponse response = await handler.Handle(query, ct);

		// Assert
		Assert.Equal(download, response);
	}
}
