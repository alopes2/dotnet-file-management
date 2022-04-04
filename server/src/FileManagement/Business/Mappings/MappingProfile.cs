using AutoMapper;
using FileManagement.Core.Models;
using FileManagement.Data.Models;

namespace FileManagement.Business.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Domain to Entity
        CreateMap<UploadImage, ImageEntity>();

        // Entity to Domain
        CreateMap<ImageEntity, Image>();
    }
}