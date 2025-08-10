using CustomCADs.Catalog.Application.Products.Commands.Internal.Designer.SetStatus;
using CustomCADs.Catalog.Domain.Products.Enums;
using CustomCADs.Shared.Endpoints.Extensions;

namespace CustomCADs.Catalog.Endpoints.Products.Endpoints.Designer.Patch.Validate;

public sealed class ValidateProductEndpoint(IRequestSender sender)
	: Endpoint<ValidateProductRequest>
{
	public override void Configure()
	{
		Patch("validate");
		Group<DesignerGroup>();
		Description(d => d
			.WithSummary("Validate")
			.WithDescription("Set a Product's Status to Validated")
		);
	}

	public override async Task HandleAsync(ValidateProductRequest req, CancellationToken ct)
	{
		await sender.SendCommandAsync(
			new SetProductStatusCommand(
				Id: ProductId.New(req.Id),
				Status: ProductStatus.Validated,
				DesignerId: User.GetAccountId()
			),
			ct
		).ConfigureAwait(false);

		await SendNoContentAsync().ConfigureAwait(false);
	}
}
