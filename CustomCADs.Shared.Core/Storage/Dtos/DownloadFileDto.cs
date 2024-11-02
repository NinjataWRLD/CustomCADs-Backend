namespace CustomCADs.Shared.Core.Storage.Dtos;

public record DownloadFileDto(
    Stream ResponseStream, 
    string ContentType, 
    string FileName
);