using CustomCADs.Customs.Application.Customs.Queries.Internal.Customers.GetCadUrlGet;
using CustomCADs.Customs.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.Core.Common.Dtos;
using CustomCADs.Shared.Core.Common.Exceptions.Application;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.UseCases.Cads.Queries;

namespace CustomCADs.UnitTests.Customs.Application.Customs.Queries.GetCadUrlGet;

using static CustomsData;

public class GetCustomCadPresignedUrlGetHandlerUnitTests : CustomsBaseUnitTests
{
    private readonly Mock<ICustomReads> reads = new();
    private readonly Mock<IRequestSender> sender = new();
    private static readonly DownloadFileResponse cad = new("presigned-url", "application/png");
    private static readonly CustomId id = ValidId1;
    private static readonly AccountId buyerId = ValidBuyerId1;
    private static readonly AccountId wrongBuyerId = ValidBuyerId2;
    private readonly Custom custom = CreateCustomWithId(id);

    public GetCustomCadPresignedUrlGetHandlerUnitTests()
    {
        custom.Accept(ValidDesignerId1);
        custom.Begin();
        custom.Finish(ValidCadId1, ValidPrice1);
        custom.Complete(ValidCustomizationId1);

        reads.Setup(x => x.SingleByIdAsync(id, false, ct))
            .ReturnsAsync(custom);

        sender.Setup(x => x.SendQueryAsync(It.IsAny<GetCadPresignedUrlGetByIdQuery>(), ct))
            .ReturnsAsync(cad);
    }

    [Fact]
    public async Task Handle_ShouldQueryDatabase()
    {
        // Arrange
        GetCustomCadPresignedUrlGetQuery query = new(
            Id: id,
            BuyerId: buyerId
        );
        GetCustomCadPresignedUrlGetHandler handler = new(reads.Object, sender.Object);

        // Act
        await handler.Handle(query, ct);

        // Assert
        reads.Verify(x => x.SingleByIdAsync(id, false, ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldSendRequests()
    {
        // Arrange
        GetCustomCadPresignedUrlGetQuery query = new(
            Id: id,
            BuyerId: buyerId
        );
        GetCustomCadPresignedUrlGetHandler handler = new(reads.Object, sender.Object);

        // Act
        await handler.Handle(query, ct);

        // Assert
        sender.Verify(x => x.SendQueryAsync(
            It.IsAny<GetCadPresignedUrlGetByIdQuery>()
        , ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnProperly()
    {
        // Arrange
        GetCustomCadPresignedUrlGetQuery query = new(
            Id: id,
            BuyerId: buyerId
        );
        GetCustomCadPresignedUrlGetHandler handler = new(reads.Object, sender.Object);

        // Act
        var result = await handler.Handle(query, ct);

        // Assert
        Assert.Equal(cad, result);
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenBuyerNotAssociated()
    {
        // Arrange
        GetCustomCadPresignedUrlGetQuery query = new(
            Id: id,
            BuyerId: wrongBuyerId
        );
        GetCustomCadPresignedUrlGetHandler handler = new(reads.Object, sender.Object);

        // Assert
        await Assert.ThrowsAsync<CustomAuthorizationException<Custom>>(async () =>
        {
            // Act
            await handler.Handle(query, ct);
        });
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenCustomNotFound()
    {
        // Arrange
        reads.Setup(x => x.SingleByIdAsync(id, false, ct))
            .ReturnsAsync(null as Custom);

        GetCustomCadPresignedUrlGetQuery query = new(
            Id: id,
            BuyerId: buyerId
        );
        GetCustomCadPresignedUrlGetHandler handler = new(reads.Object, sender.Object);

        // Assert
        await Assert.ThrowsAsync<CustomNotFoundException<Custom>>(async () =>
        {
            // Act
            await handler.Handle(query, ct);
        });
    }
}
