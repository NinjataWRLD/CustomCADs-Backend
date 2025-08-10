using CustomCADs.Catalog.Application.Products.BackgroundJobs;
using CustomCADs.Catalog.Domain.Repositories;
using CustomCADs.Catalog.Domain.Repositories.Reads;

namespace CustomCADs.UnitTests.Catalog.Application.Products.BackgroundJobs;

using static ProductConstants;
using static ProductsData;

public class ClearTagsJobUnitTests : ProductsBaseUnitTests
{
	private readonly ClearTagsJob job;
	private readonly Mock<IProductReads> reads = new();
	private readonly Mock<IUnitOfWork> uow = new();

	private const string tag = Constants.Tags.New;
	private static readonly ProductId[] ids = [];
	private static readonly DateTimeOffset today = DateTimeOffset.UtcNow.Date;
	private readonly Product oldestByTag = CreateProductWithId(
		uploadedAt: today.AddDays(-(ClearTagsBeforeDays + 10))
	);


	public ClearTagsJobUnitTests()
	{
		job = new(reads.Object, uow.Object);

		reads.Setup(x => x.OldestByTagAsync(tag, ct)).ReturnsAsync(oldestByTag);
		reads.Setup(x => x.AllAsync(
			It.Is<DateTimeOffset>(x =>
				(today - x.Date).TotalDays == ClearTagsBeforeDays
				|| (today - x.Date.AddDays(-1)).TotalDays == ClearTagsBeforeDays // for flakiness
			),
			oldestByTag.UploadedAt,
			ct
		)).ReturnsAsync(ids);
	}

	[Fact]
	public async Task Execute_ShouldQueryDatabase()
	{
		// Arrange
		Mock<Quartz.IJobExecutionContext> context = new();

		// Act
		await job.Execute(context.Object);

		// Assert
		reads.Verify(x => x.OldestByTagAsync(tag, ct), Times.Once());
		reads.Verify(x => x.AllAsync(
			It.Is<DateTimeOffset>(x =>
				(today - x.Date).TotalDays == ClearTagsBeforeDays
				|| (today - x.Date.AddDays(-1)).TotalDays == ClearTagsBeforeDays // for flakiness
			),
			oldestByTag.UploadedAt,
			ct
		), Times.Once());
	}

	[Fact]
	public async Task Execute_ShouldPersistToDatabase()
	{
		// Arrange
		Mock<Quartz.IJobExecutionContext> context = new();

		// Act
		await job.Execute(context.Object);

		// Assert
		uow.Verify(x => x.ClearProductTagsAsync(ids, tag, ct), Times.Once());
	}

	[Fact]
	public async Task Execute_ShouldShortCurcuit_WhenNoProductWithTag()
	{
		// Arrange
		reads.Setup(x => x.OldestByTagAsync(tag, ct)).ReturnsAsync(null as Product);
		Mock<Quartz.IJobExecutionContext> context = new();

		// Act
		await job.Execute(context.Object);

		// Assert
		reads.Verify(x => x.SingleByIdAsync(ValidId, true, ct), Times.Never());
		uow.Verify(x => x.ClearProductTagsAsync(ids, tag, ct), Times.Never());
	}
}
