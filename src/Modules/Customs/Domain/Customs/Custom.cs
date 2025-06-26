using CustomCADs.Customs.Domain.Customs.Enums;
using CustomCADs.Customs.Domain.Customs.States;
using CustomCADs.Customs.Domain.Customs.States.Entities;
using CustomCADs.Customs.Domain.Customs.States.Implementations;
using CustomCADs.Shared.Core.Bases.Entities;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Customizations;
using CustomCADs.Shared.Core.Common.TypedIds.Delivery;
using CustomCADs.Shared.Core.Common.TypedIds.Files;

namespace CustomCADs.Customs.Domain.Customs;

public class Custom : BaseAggregateRoot
{
	private ICustomState? state;
	private ICustomState State => state ??= RestoreState();

	private Custom() { }
	private Custom(string name, string description, bool delivery, AccountId buyerId) : this()
	{
		Name = name;
		Description = description;
		OrderedAt = DateTimeOffset.UtcNow;
		CustomStatus = CustomStatus.Pending;
		BuyerId = buyerId;
		ForDelivery = delivery;
	}

	public CustomId Id { get; init; }
	public string Name { get; private set; } = string.Empty;
	public string Description { get; private set; } = string.Empty;
	public bool ForDelivery { get; private set; }
	public CustomStatus CustomStatus { get; set; }
	public DateTimeOffset OrderedAt { get; }
	public AccountId BuyerId { get; private set; }
	public AcceptedCustom? AcceptedCustom { get; private set; }
	public FinishedCustom? FinishedCustom { get; private set; }
	public CompletedCustom? CompletedCustom { get; private set; }

	public static Custom Create(
		string name,
		string description,
		bool forDelivery,
		AccountId buyerId
	) => new Custom(name, description, forDelivery, buyerId)
			.ValidateName()
			.ValidateDescription();

	public static Custom CreateWithId(
		CustomId id,
		string name,
		string description,
		bool delivery,
		AccountId buyerId
	) => new Custom(name, description, delivery, buyerId)
	{
		Id = id
	}
	.ValidateName()
	.ValidateDescription();

	public Custom SetName(string name)
	{
		if (CustomStatus is not (CustomStatus.Pending or CustomStatus.Accepted or CustomStatus.Begun))
		{
			throw CustomValidationException<Custom>.Status(CustomStatus);
		}

		Name = name;
		this.ValidateName();

		return this;
	}

	public Custom SetDescription(string description)
	{
		if (CustomStatus is not (CustomStatus.Pending or CustomStatus.Accepted))
		{
			throw CustomValidationException<Custom>.Status(CustomStatus);
		}

		Description = description;
		this.ValidateDescription();

		return this;
	}

	public Custom SetDelivery(bool value)
	{
		if (CustomStatus is not (CustomStatus.Pending or CustomStatus.Accepted or CustomStatus.Begun or CustomStatus.Finished))
		{
			throw CustomValidationException<Custom>.Status(CustomStatus);
		}
		ForDelivery = value;

		return this;
	}

	public Custom SetShipment(ShipmentId shipmentId)
	{
		if (CustomStatus is not CustomStatus.Completed)
		{
			throw CustomValidationException<Custom>.Status(CustomStatus);
		}
		CompletedCustom!.SetShipmentId(shipmentId);

		return this;
	}


	public Custom FinishPayment(bool success = true)
	{
		if (CustomStatus is not CustomStatus.Completed)
		{
			throw CustomValidationException<Custom>.Status(CustomStatus);
		}
		CompletedCustom!.FinishPayment(success);

		return this;
	}

	public void Accept(AccountId designerId) => State.Accept(this, designerId);
	public void Begin() => State.Begin(this);
	public void Finish(CadId cadId, decimal price) => State.Finish(this, cadId, price);
	public void Complete(CustomizationId? customizationId) => State.Complete(this, customizationId);
	public void Cancel() => State.Cancel(this);
	public void Report() => State.Report(this);

	internal void SetState(ICustomState newState)
	{
		CustomStatus = newState.Status;
		state = newState;
	}

	internal Custom ClearAcceptInfo()
	{
		AcceptedCustom = null;
		return this;
	}

	internal Custom FillAcceptInfo(AccountId designerId)
	{
		AcceptedCustom = AcceptedCustom.Create(Id, designerId);
		return this;
	}

	internal Custom FillFinishInfo(CadId cadId, decimal price)
	{
		FinishedCustom = FinishedCustom.Create(Id, price, cadId);
		return this;
	}

	internal Custom FillCompleteInfo(CustomizationId? customizationId)
	{
		if (!ForDelivery)
		{
			CompletedCustom = CompletedCustom.Create(Id);
		}
		else if (customizationId is not null)
		{
			CompletedCustom = CompletedCustom.Create(Id, customizationId.Value);
		}
		else
		{
			throw CustomValidationException<Custom>.Custom("You must provide a CustomizationId when completing a Custom for delivery.");
		}

		return this;
	}

	private ICustomState RestoreState()
		=> CustomStatus switch
		{
			CustomStatus.Pending => new PendingCustomState(),
			CustomStatus.Accepted => new AcceptedCustomState(),
			CustomStatus.Begun => new BegunCustomState(),
			CustomStatus.Finished => new FinishedCustomState(),
			CustomStatus.Completed => new CompletedCustomState(),
			CustomStatus.Reported => new ReportedCustomState(),
			_ => throw CustomValidationException<Custom>.Custom($"Unknown CustomStatus: {CustomStatus}")
		};
}
