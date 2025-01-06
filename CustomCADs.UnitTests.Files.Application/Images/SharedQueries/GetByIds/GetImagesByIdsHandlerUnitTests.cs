﻿using CustomCADs.Files.Application.Images.SharedQueryHandlers;
using CustomCADs.Files.Domain.Images.Reads;
using CustomCADs.Shared.Core.Common;
using CustomCADs.Shared.Core.Common.TypedIds.Files;
using CustomCADs.Shared.UseCases.Images.Queries;

namespace CustomCADs.UnitTests.Files.Application.Images.SharedQueries.GetByIds;

using static ImagesData;

public class GetImagesByIdsHandlerUnitTests : ImagesBaseUnitTests
{
    private readonly Mock<IImageReads> reads = new();
    private readonly static ImageId[] ids = [id1, id2, id1, id2];
    private static readonly (string Key, string ContentType)[] images = [
        (ValidKey1, ValidContentType1),
        (ValidKey2, ValidContentType2),
    ];
    private static readonly ImageQuery imageQuery = new(
        Pagination: new(1, ids.Length), 
        Ids: ids
    );
    private static readonly Result<Image> result = new(
        Count: ids.Length,
        Items: [
            CreateImage(ValidKey1, ValidContentType1),
            CreateImage(ValidKey2, ValidContentType2),
        ]
    );

    [Fact]
    public async Task Handle_ShouldQueryDatabase()
    {
        // Arrange
        reads.Setup(x => x.AllAsync(imageQuery, false, ct))
            .ReturnsAsync(result);

        GetImagesByIdsQuery query = new(ids);
        GetImagesByIdsHandler handler = new(reads.Object);

        // Act
        await handler.Handle(query, ct);

        // Assert
        reads.Verify(x => x.AllAsync(imageQuery, false, ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnProperly()
    {
        // Arrange
        reads.Setup(x => x.AllAsync(imageQuery, false, ct))
            .ReturnsAsync(result);

        GetImagesByIdsQuery query = new(ids);
        GetImagesByIdsHandler handler = new(reads.Object);

        // Act
        var actualImages = (await handler.Handle(query, ct)).Select(i => (i.Key, i.ContentType));

        // Assert
        Assert.Equal(actualImages, images);
    }
}
