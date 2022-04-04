using AutoMapper;
using FileManagement.API.Models;
using FileManagement.Core.Models;

namespace PlayedIt.Api.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Domain to Resource
        CreateMap<Image, ImageResource>();

        // Resource to Domain
        CreateMap<IFormFile, UploadImage>()
            .ForMember(
                image => image.Name,
                opt => opt.MapFrom(file => Path.GetFileNameWithoutExtension(file.FileName)))
            .ForMember(
                image => image.Extension,
                opt => opt.MapFrom(file => Path.GetExtension(file.FileName)))
            .ForMember(
                image => image.File,
                opt => opt.MapFrom((file, image, byteArray) => 
                {
                    using var target = new MemoryStream();
                    file.CopyTo(target);
                    return target.ToArray();
                }));
    }
}