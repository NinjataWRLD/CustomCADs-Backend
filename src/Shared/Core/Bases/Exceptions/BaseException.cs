namespace CustomCADs.Shared.Core.Bases.Exceptions;

public class BaseException(string message, Exception? inner) : Exception(message, inner);
