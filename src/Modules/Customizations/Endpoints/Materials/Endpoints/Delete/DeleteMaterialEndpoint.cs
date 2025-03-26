using CustomCADs.Customizations.Application.Materials.Commands.Internal.Delete;
using CustomCADs.Customizations.Endpoints.Materials.Endpoints;

namespace CustomCADs.Customizations.Endpoints.Materials.Endpoints.Delete;

public sealed class DeleteMaterialEndpoint(IRequestSender sender)
    : Endpoint<DeleteMaterialRequest>
{
    public override void Configure()
    {
        Delete("");
        Group<MaterialsGroup>();
        Description(d => d
            .WithSummary("5. Delete")
            .WithDescription("Delete a Material")
        );
    }

    public override async Task HandleAsync(DeleteMaterialRequest req, CancellationToken ct)
    {
        DeleteMaterialCommand command = new(
            Id: MaterialId.New(req.Id)
        );
        await sender.SendCommandAsync(command, ct).ConfigureAwait(false);

        await SendNoContentAsync().ConfigureAwait(false);
    }
}
