﻿using CustomCADs.Orders.Domain.Repositories;
using CustomCADs.Orders.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.Core.Common.TypedIds.Files;
using CustomCADs.Shared.UseCases.Cads.Commands;

namespace CustomCADs.Orders.Application.OngoingOrders.Commands.Internal.Status.Finish;

public sealed class FinishOngoingOrderHandler(IOngoingOrderReads reads, IUnitOfWork uow, IRequestSender sender)
    : ICommandHandler<FinishOngoingOrderCommand>
{
    public async Task Handle(FinishOngoingOrderCommand req, CancellationToken ct)
    {
        OngoingOrder order = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
            ?? throw CustomNotFoundException<OngoingOrder>.ById(req.Id);

        if (req.DesignerId != order.DesignerId)
            throw CustomAuthorizationException<OngoingOrder>.ById(req.Id);

        order.SetPrice(req.Price);
        order.SetFinishedStatus();

        CreateCadCommand cadCommand = new(
            Key: req.Cad.Key,
            ContentType: req.Cad.ContentType,
            Volume: req.Cad.Volume
        );
        CadId cadId = await sender.SendCommandAsync(cadCommand, ct).ConfigureAwait(false);
        order.SetCadId(cadId);

        await uow.SaveChangesAsync(ct).ConfigureAwait(false);
    }
}
