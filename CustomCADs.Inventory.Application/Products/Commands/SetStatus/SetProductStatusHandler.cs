﻿using CustomCADs.Inventory.Application.Common.Exceptions;
using CustomCADs.Inventory.Domain.Common;
using CustomCADs.Inventory.Domain.Common.Exceptions.Products;
using CustomCADs.Inventory.Domain.Products;
using CustomCADs.Inventory.Domain.Products.Enums;
using CustomCADs.Inventory.Domain.Products.Reads;

namespace CustomCADs.Inventory.Application.Products.Commands.SetStatus;

public class SetProductStatusHandler(IProductReads reads, IUnitOfWork uow)
    : ICommandHandler<SetProductStatusCommand>
{
    public async Task Handle(SetProductStatusCommand req, CancellationToken ct)
    {
        Product product = await reads.SingleByIdAsync(req.Id, ct: ct)
            ?? throw ProductNotFoundException.ById(req.Id);

        if (product.DesignerId is not null)
        {
            throw ProductAuthorizationException.AlreadyChecked();
        }
        product.SetDesignerId(req.DesignerId);

        switch (req.Status)
        {
            case ProductStatus.Validated: product.SetValidatedStatus(); break;
            case ProductStatus.Reported: product.SetReportedStatus(); break;
            default: throw ProductValidationException.InvalidStatus(req.Id, product.Status, req.Status);
        }

        await uow.SaveChangesAsync(ct).ConfigureAwait(false);
    }
}