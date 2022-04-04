using FileManagement.Data.Configurations;
using FileManagement.Data.Interfaces;
using FileManagement.Data.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace FileManagement.Data.Repositories;

public class ImageRepository : IImageRepository
{
    private readonly MongoDBOptions _mongoDbOptions;

    private readonly IMongoCollection<ImageEntity> _collection;

    public ImageRepository(IOptions<MongoDBOptions> mongoDbOptionsSnapshot)
    {
        _mongoDbOptions = mongoDbOptionsSnapshot.Value;

        var mongoClient = new MongoClient(_mongoDbOptions.ConnectionString);

        var database = mongoClient.GetDatabase(_mongoDbOptions.ImagesDatabase);

        _collection = database.GetCollection<ImageEntity>(_mongoDbOptions.ImagesCollection);
    }

    public async Task<ImageEntity> AddImage(ImageEntity image)
    {
        // The insert will update the image object with a generated ID
        await _collection.InsertOneAsync(image);
        return image;
    }

    public async Task<IEnumerable<ImageEntity>> GetImages()
    {
        var images = await _collection
            .FindAsync(_ => true);

        return await images.ToListAsync();
    }
}