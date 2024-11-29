using CustomCADs.Shared.Application.Requests.Commands;
using FluentValidation;

namespace CustomCADs.Shared.Application.Requests.Validator;

public class Validator<TCommand> : AbstractValidator<TCommand> where TCommand : ICommand;
public class Validator<TCommand, TResponse> : AbstractValidator<TCommand> where TCommand : ICommand<TResponse>;
