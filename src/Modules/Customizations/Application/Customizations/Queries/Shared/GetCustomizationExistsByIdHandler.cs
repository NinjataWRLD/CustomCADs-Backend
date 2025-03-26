using CustomCADs.Customizations.Domain.Repositories.Reads;
using CustomCADs.Shared.UseCases.Customizations.Queries;

namespace CustomCADs.Customizations.Application.Customizations.Queries.Shared;

public class GetCustomizationExistsByIdHandler(ICustomizationReads reads)
    : IQueryHandler<GetCustomizationExistsByIdQuery, bool>
{
    public async Task<bool> Handle(GetCustomizationExistsByIdQuery req, CancellationToken ct)
    {
        return await reads.ExistsByIdAsync(req.Id, ct).ConfigureAwait(false);
    }
}
