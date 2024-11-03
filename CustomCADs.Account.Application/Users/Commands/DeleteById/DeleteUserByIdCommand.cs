using CustomCADs.Account.Application.Common.Contracts;

namespace CustomCADs.Account.Application.Users.Commands.DeleteById;

public record DeleteUserByIdCommand(Guid Id) : ICommand;