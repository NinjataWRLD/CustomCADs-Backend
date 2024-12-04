﻿using CustomCADs.Shared.Core.Bases.Exceptions;

namespace CustomCADs.Shared.Application.Payment.Exceptions;

public class PaymentFailedException : BaseException
{
    private PaymentFailedException(string message, string? clientSecret, Exception? inner) : base(message, inner) { }

    public string? ClientSecret { get; }

    public static PaymentFailedException General(string message, Exception? inner = default)
        => new(message, null, inner);

    public static PaymentFailedException WithClientSecret(string clientSecret, string message, Exception? inner = default)
        => new(message, clientSecret, inner);
}