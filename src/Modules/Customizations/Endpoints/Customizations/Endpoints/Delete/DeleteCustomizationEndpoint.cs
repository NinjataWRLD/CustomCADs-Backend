﻿using CustomCADs.Customizations.Application.Customizations.Commands.Internal.Delete;

namespace CustomCADs.Customizations.Endpoints.Customizations.Endpoints.Delete;

public class DeleteCustomizationEndpoint(IRequestSender sender)
    : Endpoint<DeleteCustomizationRequest>
{
    public override void Configure()
    {
        Delete("");
        Group<CustomizationsGroup>();
        Description(d => d
            .WithSummary("Delete")
            .WithDescription("Delete your Customization")
        );
    }

    public override async Task HandleAsync(DeleteCustomizationRequest req, CancellationToken ct)
    {
        DeleteCustomizationCommand command = new(
            Id: CustomizationId.New(req.Id)
        );
        await sender.SendCommandAsync(command, ct).ConfigureAwait(false);

        await SendNoContentAsync().ConfigureAwait(false);
    }
}
