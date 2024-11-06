namespace CustomCADs.Shared.Core.Storage.Dtos;

public record FileDto(byte[] Bytes, string FileName, string ContentType);
