﻿using CustomCADs.Shared.Core.Bases.Exceptions;

namespace CustomCADs.Shared.Core.Common.Exceptions.Persistence;

public class DatabaseException : BaseException
{
	private DatabaseException(string message, Exception? inner) : base(message, inner) { }

	public static DatabaseException General(Exception? inner = null)
		=> new("The saving of changes to the Database failed.", inner);

	public static DatabaseException Custom(string message, Exception? inner = null)
		=> new(message, inner);
}
