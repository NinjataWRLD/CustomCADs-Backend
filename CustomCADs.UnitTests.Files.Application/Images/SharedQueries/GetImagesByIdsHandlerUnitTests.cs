using CustomCADs.Files.Application.Images.SharedQueryHandlers;
using CustomCADs.Files.Domain.Images.Reads;
using CustomCADs.Shared.Core.Common;
using CustomCADs.Shared.Core.Common.TypedIds.Files;
using CustomCADs.Shared.UseCases.Images.Queries;

namespace CustomCADs.UnitTests.Files.Application.Images.SharedQueries;

public class GetImagesByIdsHandlerUnitTests : ImagesBaseUnitTests
{
    private readonly IImageReads reads = Substitute.For<IImageReads>();
    private const string Id1 = "e3f5e3f5-e3f5-e3f5-e3f5-e3f5e3f5e3f5";
    private const string Id2 = "e3f5e3f5-e3f5-e3f5-e3f5-e3f5e3f5e3f5";
    private static readonly (string Key, string ContentType)[] images = [
        (ValidKey1, ValidContentType1),
        (ValidKey2, ValidContentType2),
    ];

    [Theory]
    [InlineData(Id1, Id2)]
    public async Task Handle_CallsDatabase(params string[] ids)
    {
        // Arrange
        ImageId[] imageIds = [.. ids.Select(id => new ImageId(Guid.Parse(id)))];
        ImageQuery imageQuery = new(Pagination: new(1, imageIds.Length), Ids: imageIds);

        reads.AllAsync(imageQuery, false, ct).Returns(new Result<Image>(
            Count: imageIds.Length,
            Items: [
                CreateImage(ValidKey1, ValidContentType1),
                CreateImage(ValidKey2, ValidContentType2),
            ]
        ));

        GetImagesByIdsQuery query = new(imageIds);
        GetImagesByIdsHandler handler = new(reads);

        // Act
        await handler.Handle(query, ct);

        // Assert
        await reads.Received(1).AllAsync(imageQuery, false, ct);
    }
    
    [Theory]
    [InlineData(Id1, Id2)]
    public async Task Handle_ShouldReturnProperly(params string[] ids)
    {
        // Arrange
        ImageId[] imageIds = [.. ids.Select(id => new ImageId(Guid.Parse(id)))];
        ImageQuery imageQuery = new(Pagination: new(1, imageIds.Length), Ids: imageIds);

        reads.AllAsync(imageQuery, false, ct).Returns(new Result<Image>(
            Count: imageIds.Length,
            Items: [
                CreateImage(ValidKey1, ValidContentType1),
                CreateImage(ValidKey2, ValidContentType2),
            ]
        ));

        GetImagesByIdsQuery query = new(imageIds);
        GetImagesByIdsHandler handler = new(reads);

        // Act
        var actualImages = (await handler.Handle(query, ct)).Select(i => (i.Key, i.ContentType));

        // Assert
        Assert.Equal(actualImages, images);
    }
}
