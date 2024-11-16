namespace CustomCADs.Shared.Speedy.Services;

public static class Utilities
{
    public static void EnsureNull(this ErrorDto? error)
    {
        if (error is null) return;

        throw error.Code switch
        {
            1 => new(error.Message),
            100 => new(error.Message),
            120 => new(error.Message),
            160 => new(error.Message),
            180 => new(error.Message),
            300 => new(error.Message),
            400 => new(error.Message),
            410 => new(error.Message),
            415 => new(error.Message),
            420 => new(error.Message),
            425 => new(error.Message),
            430 => new(error.Message),
            435 => new(error.Message),
            440 => new(error.Message),
            445 => new(error.Message),
            450 => new(error.Message),
            455 => new(error.Message),
            460 => new(error.Message),
            500 => new(error.Message),
            510 => new(error.Message),
            515 => new(error.Message),
            520 => new(error.Message),
            600 => new(error.Message),
            605 => new(error.Message),
            610 => new(error.Message),
            615 => new(error.Message),
            620 => new(error.Message),
            630 => new(error.Message),
            700 => new(error.Message),
            710 => new(error.Message),
            800 => new(error.Message),
            805 => new(error.Message),
            810 => new(error.Message),
            _ => new(error.Message),
        };
    }
}
