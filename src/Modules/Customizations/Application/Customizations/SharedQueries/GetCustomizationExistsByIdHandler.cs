﻿using CustomCADs.Customizations.Domain.Customizations.Reads;
using CustomCADs.Shared.UseCases.Customizations.Queries;

namespace CustomCADs.Customizations.Application.Customizations.SharedQueries;

public class GetCustomizationExistsByIdHandler(ICustomizationReads reads)
    : IQueryHandler<GetCustomizationExistsByIdQuery, bool>
{
    public async Task<bool> Handle(GetCustomizationExistsByIdQuery req, CancellationToken ct)
    {
        return await reads.ExistsByIdAsync(req.Id, ct).ConfigureAwait(false);
    }
}
