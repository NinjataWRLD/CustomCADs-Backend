using CustomCADs.Categories.Application.Categories.Queries.Shared;
using CustomCADs.Categories.Domain.Repositories.Reads;
using CustomCADs.Shared.Core.Common.Exceptions.Application;
using CustomCADs.Shared.Core.Common.TypedIds.Categories;
using CustomCADs.Shared.UseCases.Categories.Queries;
using CustomCADs.UnitTests.Categories.Application.Categories.Queries.Shared.GetById.Data;

namespace CustomCADs.UnitTests.Categories.Application.Categories.Queries.Shared.GetById;

using static CategoriesData;

public class GetCategoryByIdHandlerUnitTests : CategoriesBaseUnitTests
{
	private readonly Mock<ICategoryReads> reads = new();

	public GetCategoryByIdHandlerUnitTests()
	{
		reads.Setup(v => v.SingleByIdAsync(ValidId1, false, ct))
			.ReturnsAsync(CreateCategory());

		reads.Setup(v => v.SingleByIdAsync(ValidId2, false, ct))
			.ReturnsAsync(CreateCategory());

		reads.Setup(v => v.SingleByIdAsync(ValidId3, false, ct))
			.ReturnsAsync(CreateCategory());
	}

	[Theory]
	[ClassData(typeof(GetCategoryByIdValidData))]
	public async Task Handle_ShouldQueryDatabase(CategoryId id)
	{
		// Assert
		GetCategoryNameByIdQuery query = new(id);
		GetCategoryNameByIdHandler handler = new(reads.Object);

		// Act
		await handler.Handle(query, ct);

		// Assert
		reads.Verify(v => v.SingleByIdAsync(id, false, ct), Times.Once());
	}

	[Theory]
	[ClassData(typeof(GetCategoryByIdValidData))]
	public async Task Handle_ShouldThrowException_WhenDatabaseMiss(CategoryId id)
	{
		// Assert
		reads.Setup(v => v.SingleByIdAsync(id, false, ct)).ReturnsAsync(null as Category);

		// Assert
		GetCategoryNameByIdQuery query = new(id);
		GetCategoryNameByIdHandler handler = new(reads.Object);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Category>>(async () =>
		{
			// Act
			await handler.Handle(query, ct);
		});

	}
}
