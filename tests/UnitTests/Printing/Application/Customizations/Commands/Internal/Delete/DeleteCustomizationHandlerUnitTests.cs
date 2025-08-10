using CustomCADs.Printing.Application.Customizations.Commands.Shared.Delete;
using CustomCADs.Printing.Domain.Customizations;
using CustomCADs.Printing.Domain.Repositories;
using CustomCADs.Printing.Domain.Repositories.Reads;
using CustomCADs.Shared.Core.Common.Exceptions.Application;
using CustomCADs.Shared.UseCases.Printing.Commands;

namespace CustomCADs.UnitTests.Printing.Application.Customizations.Commands.Internal.Delete;

using static CustomizationsData;

public class DeleteCustomizationHandlerUnitTests : CustomizationsBaseUnitTests
{
	private readonly DeleteCustomizationHandler handler;
	private readonly Mock<ICustomizationReads> reads = new();
	private readonly Mock<IWrites<Customization>> writes = new();
	private readonly Mock<IUnitOfWork> uow = new();

	private readonly Customization customization = CreateCustomization();

	public DeleteCustomizationHandlerUnitTests()
	{
		handler = new(reads.Object, writes.Object, uow.Object);

		reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct))
			.ReturnsAsync(customization);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		DeleteCustomizationByIdCommand command = new(ValidId);

		// Act
		await handler.Handle(command, ct);

		// Assert
		reads.Verify(x => x.SingleByIdAsync(ValidId, true, ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldPersistToDatabase()
	{
		// Arrange
		DeleteCustomizationByIdCommand command = new(ValidId);

		// Act
		await handler.Handle(command, ct);

		// Assert
		writes.Verify(x => x.Remove(customization), Times.Once());
		uow.Verify(x => x.SaveChangesAsync(ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenCustomizationNotFound()
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct)).ReturnsAsync(null as Customization);
		DeleteCustomizationByIdCommand command = new(ValidId);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Customization>>(
			// Act
			async () => await handler.Handle(command, ct)
		);
	}
}
