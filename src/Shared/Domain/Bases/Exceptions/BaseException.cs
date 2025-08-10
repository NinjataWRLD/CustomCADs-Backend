namespace CustomCADs.Shared.Domain.Bases.Exceptions;

public class BaseException(string message, Exception? inner) : Exception(message, inner);
