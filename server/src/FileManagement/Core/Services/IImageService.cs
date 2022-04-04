using FileManagement.Core.Models;

namespace FileManagement.Core.Services;

public interface IImageService
{
    Task<Image> UploadImageAsync(UploadImage image);

    Task<IEnumerable<Image>> GetImagesAsync();
}