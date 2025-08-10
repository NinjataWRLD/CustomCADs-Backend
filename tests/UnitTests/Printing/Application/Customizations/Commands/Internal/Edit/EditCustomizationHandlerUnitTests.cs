using CustomCADs.Printing.Application.Customizations.Commands.Internal.Edit;
using CustomCADs.Printing.Domain.Customizations;
using CustomCADs.Printing.Domain.Repositories;
using CustomCADs.Printing.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Exceptions;

namespace CustomCADs.UnitTests.Printing.Application.Customizations.Commands.Internal.Edit;

using static CustomizationsData;

public class EditCustomizationHandlerUnitTests : CustomizationsBaseUnitTests
{
	private readonly EditCustomizationHandler handler;
	private readonly Mock<ICustomizationReads> reads = new();
	private readonly Mock<IUnitOfWork> uow = new();

	private readonly Customization customization = CreateCustomization();

	public EditCustomizationHandlerUnitTests()
	{
		handler = new(reads.Object, uow.Object);

		reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct))
			.ReturnsAsync(customization);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		EditCustomizationCommand command = new(
			Id: ValidId,
			Scale: MaxValidScale,
			Infill: MaxValidInfill,
			Volume: MaxValidVolume,
			Color: ValidColor,
			MaterialId: ValidMaterialId
		);

		// Act
		await handler.Handle(command, ct);

		// Assert
		reads.Verify(x => x.SingleByIdAsync(ValidId, true, ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldPersistToDatabase()
	{
		// Arrange
		EditCustomizationCommand command = new(
			Id: ValidId,
			Scale: MaxValidScale,
			Infill: MaxValidInfill,
			Volume: MaxValidVolume,
			Color: ValidColor,
			MaterialId: ValidMaterialId
		);

		// Act
		await handler.Handle(command, ct);

		// Assert
		uow.Verify(x => x.SaveChangesAsync(ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenCustomizationNotFound()
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct)).ReturnsAsync(null as Customization);
		EditCustomizationCommand command = new(
			Id: ValidId,
			Scale: MaxValidScale,
			Infill: MaxValidInfill,
			Volume: MaxValidVolume,
			Color: ValidColor,
			MaterialId: ValidMaterialId
		);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Customization>>(
			// Act
			async () => await handler.Handle(command, ct)
		);
	}
}
