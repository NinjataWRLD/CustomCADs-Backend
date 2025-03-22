﻿using CustomCADs.Orders.Domain.Repositories;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.UseCases.Accounts.Queries;
using CustomCADs.Shared.UseCases.Cads.Queries;
using CustomCADs.Shared.UseCases.Customizations.Queries;

namespace CustomCADs.Orders.Application.CompletedOrders.Commands.Create;

public class CreateCompletedOrderHandler(IWrites<CompletedOrder> writes, IUnitOfWork uow, IRequestSender sender)
    : ICommandHandler<CreateCompletedOrderCommand, CompletedOrderId>
{
    public async Task<CompletedOrderId> Handle(CreateCompletedOrderCommand req, CancellationToken ct)
    {
        GetAccountExistsByIdQuery buyerQuery = new(req.BuyerId);
        bool buyerExists = await sender.SendQueryAsync(buyerQuery, ct).ConfigureAwait(false);
        if (!buyerExists)
        {
            throw CompletedOrderNotFoundException.BuyerId(req.BuyerId);
        }

        GetAccountExistsByIdQuery designerQuery = new(req.DesignerId);
        bool designerExists = await sender.SendQueryAsync(designerQuery, ct).ConfigureAwait(false);
        if (!designerExists)
        {
            throw CompletedOrderNotFoundException.DesignerId(req.DesignerId);
        }

        GetCadExistsByIdQuery cadQuery = new(req.CadId);
        bool cadExists = await sender.SendQueryAsync(cadQuery, ct).ConfigureAwait(false);
        if (!cadExists)
        {
            throw CompletedOrderNotFoundException.CadId(req.CadId);
        }

        CompletedOrder order = req.ToCompletedOrder();
        if (req.CustomizationId is not null)
        {
            GetCustomizationExistsByIdQuery customizationExistsQuery = new(
                Id: req.CustomizationId.Value
            );

            bool customizationExists = await sender.SendQueryAsync(customizationExistsQuery, ct).ConfigureAwait(false);
            if (!customizationExists)
            {
                throw CompletedOrderNotFoundException.CustomizationId(req.CustomizationId.Value);
            }

            order.SetCustomizationId(req.CustomizationId.Value);
        }

        await writes.AddAsync(order, ct).ConfigureAwait(false);
        await uow.SaveChangesAsync(ct).ConfigureAwait(false);

        return order.Id;
    }
}
