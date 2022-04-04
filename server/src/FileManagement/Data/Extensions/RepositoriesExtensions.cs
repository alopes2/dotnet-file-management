using FileManagement.Data.Interfaces;
using FileManagement.Data.Repositories;

namespace FileManagement.Data.Extensions;

public static class RepositoriesExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        // We add it as singleton because it creates and uses MongoClient inside
        // As per MongoDB guidelines, MongoClient reuses the same pool everytime
        // And should be declared in a global instance
        // https://mongodb.github.io/mongo-csharp-driver/2.14/reference/driver/connecting/#re-use
        services.AddSingleton<IImageRepository, ImageRepository>();

        return services;
    }
}