using CustomCADs.Shared.Core.Bases.Entities;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Customs.Domain.Customs.States.Entities;

public class AcceptedCustom : BaseEntity
{
	private AcceptedCustom() { }
	private AcceptedCustom(CustomId customId, AccountId designerId) : this()
	{
		CustomId = customId;
		AcceptedAt = DateTimeOffset.UtcNow;
		DesignerId = designerId;
	}

	public CustomId CustomId { get; private set; }
	public DateTimeOffset AcceptedAt { get; private set; }
	public AccountId DesignerId { get; private set; }

	public static AcceptedCustom Create(CustomId customId, AccountId designerId)
		=> new(customId, designerId);

	public AcceptedCustom SetDesignerId(AccountId designerId)
	{
		DesignerId = designerId;
		return this;
	}
}
