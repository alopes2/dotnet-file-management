using FileManagement.Core.Services;
using FileManagement.Firebase.Client;
using FileManagement.Firebase.Configurations;

namespace FileManagement.Firebase.Extensions;

public static class StorageExtensions
{
    public static void AddFirebaseStorageClient(this IServiceCollection services, StorageOptions settings)
    {
        services
            .AddHttpClient<IStorageClient, StorageClient>(options =>
            {
                options.BaseAddress = new Uri(settings.BaseAddress);
            });
    }

    public static void AddFirebaseStorage(this IServiceCollection services, IConfiguration configuration)
    {
        var storageOptionsSection = configuration.GetSection(StorageOptions.Name);
        services.Configure<StorageOptions>(storageOptionsSection);

        services.AddFirebaseStorageClient(storageOptionsSection.Get<StorageOptions>());
    }
}