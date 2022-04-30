using AutoMapper;
using FileManagement.API.Models;
using FileManagement.Core.Models;
using FileManagement.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace FileManagement.API.Controllers;

[ApiController]
[Route("controller-images")]
public class ImagesController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IImageService _imageService;
    
    public ImagesController(IImageService imageService, IMapper mapper)
    {
        _imageService = imageService;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> UploadImageAsync([FromForm] UploadImageResource file)
    {
        var uploadImage = _mapper.Map<UploadImage>(file.Image);

        var image = await _imageService.UploadImageAsync(uploadImage);

        var imageResource = _mapper.Map<ImageResource>(image);

        return Created(imageResource.Url, imageResource);
    }
}