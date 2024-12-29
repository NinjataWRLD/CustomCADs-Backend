using CustomCADs.Files.Application.Images.SharedQueryHandlers;
using CustomCADs.Files.Domain.Images.Reads;
using CustomCADs.Shared.Core.Common;
using CustomCADs.Shared.Core.Common.TypedIds.Files;
using CustomCADs.Shared.UseCases.Images.Queries;

namespace CustomCADs.UnitTests.Files.Application.Images.SharedQueries.GetByIds;

using static ImagesData;

public class GetImagesByIdsHandlerUnitTests : ImagesBaseUnitTests
{
    private readonly IImageReads reads = Substitute.For<IImageReads>();
    private readonly static ImageId[] ids = [id, id, id, id];
    private static readonly (string Key, string ContentType)[] images = [
        (ValidKey1, ValidContentType1),
        (ValidKey2, ValidContentType2),
    ];

    [Fact]
    public async Task Handle_CallsDatabase()
    {
        // Arrange
        ImageQuery imageQuery = new(Pagination: new(1, ids.Length), Ids: ids);
        reads.AllAsync(imageQuery, false, ct).Returns(new Result<Image>(
            Count: ids.Length,
            Items: [
                CreateImage(ValidKey1, ValidContentType1),
                CreateImage(ValidKey2, ValidContentType2),
            ]
        ));

        GetImagesByIdsQuery query = new(ids);
        GetImagesByIdsHandler handler = new(reads);

        // Act
        await handler.Handle(query, ct);

        // Assert
        await reads.Received(1).AllAsync(imageQuery, false, ct);
    }

    [Fact]
    public async Task Handle_ShouldReturnProperly()
    {
        // Arrange
        ImageQuery imageQuery = new(Pagination: new(1, ids.Length), Ids: ids);
        reads.AllAsync(imageQuery, false, ct).Returns(new Result<Image>(
            Count: ids.Length,
            Items: [
                CreateImage(ValidKey1, ValidContentType1),
                CreateImage(ValidKey2, ValidContentType2),
            ]
        ));

        GetImagesByIdsQuery query = new(ids);
        GetImagesByIdsHandler handler = new(reads);

        // Act
        var actualImages = (await handler.Handle(query, ct)).Select(i => (i.Key, i.ContentType));

        // Assert
        Assert.Equal(actualImages, images);
    }
}
