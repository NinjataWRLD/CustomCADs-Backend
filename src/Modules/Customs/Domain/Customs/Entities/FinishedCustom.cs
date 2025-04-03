using CustomCADs.Shared.Core.Bases.Entities;
using CustomCADs.Shared.Core.Common.TypedIds.Files;

namespace CustomCADs.Customs.Domain.Customs.Entities;

public class FinishedCustom : BaseEntity
{
    private FinishedCustom() { }
    private FinishedCustom(CustomId customId, decimal price, CadId cadId)
    {
        CustomId = customId;
        Price = price;
        FinishedAt = DateTimeOffset.UtcNow;
        CadId = cadId;
    }

    public CustomId CustomId { get; set; }
    public decimal Price { get; set; }
    public DateTimeOffset FinishedAt { get; set; }
    public CadId CadId { get; set; }

    public static FinishedCustom Create(CustomId customId, decimal price, CadId cadId)
        => new FinishedCustom(customId, price, cadId)
        .ValidatePrice();
}
