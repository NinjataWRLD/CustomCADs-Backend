using CustomCADs.Shared.Core.Common.TypedIds.Account;

namespace CustomCADs.Account.Application.Users.Commands.DeleteById;

public record DeleteUserByIdCommand(UserId Id) : ICommand;