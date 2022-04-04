namespace FileManagement.Core.Services;

public interface IStorageClient
{
    Task<string> UploadImageAsync(byte[] file, string fileName);
}