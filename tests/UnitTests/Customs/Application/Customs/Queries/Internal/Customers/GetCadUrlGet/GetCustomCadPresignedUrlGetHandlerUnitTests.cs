using CustomCADs.Customs.Application.Customs.Queries.Internal.Customers.GetCadUrlGet;
using CustomCADs.Customs.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.Dtos.Files;
using CustomCADs.Shared.Application.Exceptions;
using CustomCADs.Shared.Application.UseCases.Cads.Queries;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.UnitTests.Customs.Application.Customs.Queries.GetCadUrlGet;

using static CustomsData;

public class GetCustomCadPresignedUrlGetHandlerUnitTests : CustomsBaseUnitTests
{
	private readonly GetCustomCadPresignedUrlGetHandler handler;
	private readonly Mock<ICustomReads> reads = new();
	private readonly Mock<IRequestSender> sender = new();

	private static readonly DownloadFileResponse cad = new("presigned-url", "application/png");
	private static readonly AccountId buyerId = AccountId.New();
	private readonly Custom custom = CreateCustomWithId(ValidId);

	public GetCustomCadPresignedUrlGetHandlerUnitTests()
	{
		handler = new(reads.Object, sender.Object);

		custom.Accept(ValidDesignerId);
		custom.Begin();
		custom.Finish(ValidCadId, ValidPrice);
		custom.Complete(ValidCustomizationId);
		custom.FinishPayment(success: true);

		reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct))
			.ReturnsAsync(custom);

		sender.Setup(x => x.SendQueryAsync(
			It.IsAny<GetCadPresignedUrlGetByIdQuery>(),
			ct
		)).ReturnsAsync(cad);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		GetCustomCadPresignedUrlGetQuery query = new(
			Id: ValidId,
			BuyerId: ValidBuyerId
		);

		// Act
		await handler.Handle(query, ct);

		// Assert
		reads.Verify(x => x.SingleByIdAsync(ValidId, false, ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldSendRequests()
	{
		// Arrange
		GetCustomCadPresignedUrlGetQuery query = new(
			Id: ValidId,
			BuyerId: ValidBuyerId
		);

		// Act
		await handler.Handle(query, ct);

		// Assert
		sender.Verify(x => x.SendQueryAsync(
			It.IsAny<GetCadPresignedUrlGetByIdQuery>(),
			ct
		), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange
		GetCustomCadPresignedUrlGetQuery query = new(
			Id: ValidId,
			BuyerId: ValidBuyerId
		);

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
			Id: ValidId,
			BuyerId: buyerId
		);

		// Assert
		await Assert.ThrowsAsync<CustomAuthorizationException<Custom>>(
			// Act
			async () => await handler.Handle(query, ct)
		);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenCustomNotFound()
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct))
			.ReturnsAsync(null as Custom);

		GetCustomCadPresignedUrlGetQuery query = new(
			Id: ValidId,
			BuyerId: ValidBuyerId
		);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Custom>>(
			// Act
			async () => await handler.Handle(query, ct)
		);
	}
}
