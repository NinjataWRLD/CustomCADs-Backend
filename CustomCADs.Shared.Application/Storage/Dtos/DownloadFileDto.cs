namespace CustomCADs.Shared.Application.Storage.Dtos;

public record DownloadFileDto(
    Stream ResponseStream,
    string ContentType,
    string FileName
);