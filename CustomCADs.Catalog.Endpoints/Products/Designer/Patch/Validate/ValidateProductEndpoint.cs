using CustomCADs.Catalog.Application.Products.Commands.SetStatus;
using CustomCADs.Catalog.Domain.Products.Enums;
using CustomCADs.Catalog.Endpoints.Products.Designer;

namespace CustomCADs.Catalog.Endpoints.Products.Designer.Patch.Validate;

public sealed class ValidateProductEndpoint(IRequestSender sender)
    : Endpoint<ValidateProductRequest>
{
    public override void Configure()
    {
        Patch("{id}/status");
        Group<DesignerGroup>();
        Description(d => d
            .WithSummary("03. Validate")
            .WithDescription("Set a Product's Status to Validated")
        );
    }

    public override async Task HandleAsync(ValidateProductRequest req, CancellationToken ct)
    {
        SetProductStatusCommand command = new(
            Id: new(req.Id),
            Status: ProductStatus.Validated,
            DesignerId: User.GetAccountId()
        );
        await sender.SendCommandAsync(command, ct).ConfigureAwait(false);

        await SendNoContentAsync().ConfigureAwait(false);
    }
}
