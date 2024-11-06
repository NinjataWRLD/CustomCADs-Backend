namespace CustomCADs.Shared.Infrastructure.Storage;

public record StorageSettings(string AccessKey, string SecretKey, string Region, string BucketName)
{
    public StorageSettings() : this(string.Empty, string.Empty, string.Empty, string.Empty) { }
}
