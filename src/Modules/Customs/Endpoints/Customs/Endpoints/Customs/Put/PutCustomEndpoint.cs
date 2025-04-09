﻿using CustomCADs.Customs.Application.Customs.Commands.Internal.Customers.Edit;
using CustomCADs.Customs.Endpoints.Customs.Endpoints.Customs;

namespace CustomCADs.Customs.Endpoints.Customs.Endpoints.Customs.Put;

public sealed class PutCustomEndpoint(IRequestSender sender)
    : Endpoint<PutCustomRequest>
{
    public override void Configure()
    {
        Put("");
        Group<CustomerGroup>();
        Description(d => d
            .WithSummary("Edit")
            .WithDescription("Edit your Custom")
        );
    }

    public override async Task HandleAsync(PutCustomRequest req, CancellationToken ct)
    {
        await sender.SendCommandAsync(
            new EditCustomCommand(
                Id: CustomId.New(req.Id),
                Name: req.Name,
                Description: req.Description,
                BuyerId: User.GetAccountId()
            ),
            ct
        ).ConfigureAwait(false);

        await SendNoContentAsync().ConfigureAwait(false);
    }
}
