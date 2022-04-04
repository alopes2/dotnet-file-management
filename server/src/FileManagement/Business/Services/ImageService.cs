using AutoMapper;
using FileManagement.Core.Models;
using FileManagement.Core.Services;
using FileManagement.Data.Interfaces;
using FileManagement.Data.Models;

namespace FileManagement.Business.Services;

public class ImageService : IImageService
{
    private readonly IImageRepository _imageRepository;
    private readonly IMapper _mapper;
    private readonly IStorageClient _storageClient;

    public ImageService(
        IImageRepository imageRepository,
        IMapper mapper,
        IStorageClient storageClient)
    {
        _mapper = mapper;
        _imageRepository = imageRepository;
        _storageClient = storageClient;
    }

    public async Task<Image> UploadImageAsync(UploadImage image)
    {
        var imageName = $"{Uri.EscapeDataString(image.Name)}." +
                            $"{DateTime.UtcNow.ToFileTimeUtc()}" +
                            $"{image.Extension}";

        var imageUrl = await _storageClient.UploadImageAsync(image.File, imageName);

        var imageEntity = _mapper.Map<ImageEntity>(image);
        imageEntity.Url = imageUrl;

        var addedImageEntity = await _imageRepository.AddImage(imageEntity);

        var newImage = _mapper.Map<Image>(addedImageEntity);

        return newImage;
    }

    public async Task<IEnumerable<Image>> GetImagesAsync()
    {
        var imageEntities = await _imageRepository.GetImages();

        var images = _mapper.Map<IEnumerable<Image>>(imageEntities);

        return images;
    }
}