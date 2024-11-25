using CustomCADs.Shared.Core.Common.TypedIds.Orders;

namespace CustomCADs.Orders.Application.Orders.Commands.Delete;

public record DeleteOrderCommand(OrderId Id) : ICommand;
