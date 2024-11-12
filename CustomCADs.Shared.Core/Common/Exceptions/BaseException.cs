namespace CustomCADs.Shared.Core.Common.Exceptions;

public class BaseException(string message, Exception? inner) : Exception(message, inner);
