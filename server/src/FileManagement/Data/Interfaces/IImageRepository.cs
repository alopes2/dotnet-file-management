using FileManagement.Data.Models;

namespace FileManagement.Data.Interfaces;

public interface IImageRepository
{
    Task<ImageEntity> AddImage(ImageEntity image);

    Task<IEnumerable<ImageEntity>> GetImages();
}