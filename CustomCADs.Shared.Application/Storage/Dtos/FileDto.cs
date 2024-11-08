namespace CustomCADs.Shared.Application.Storage.Dtos;

public record FileDto(byte[] Bytes, string FileName, string ContentType);
