using FileManagement.Business.Services;
using FileManagement.Core.Services;

namespace FileManagement.Business.Extensions;

public static class ServicesExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddTransient<IImageService, ImageService>();

        return services;
    }
}