using CustomCADs.Account.Application.Common.Contracts;

namespace CustomCADs.Account.Application.Users.Commands.DeleteByName;

public record DeleteUserByNameCommand(string Username) : ICommand;