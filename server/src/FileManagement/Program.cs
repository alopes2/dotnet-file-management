using AutoMapper;
using FileManagement.API.Models;
using FileManagement.Business.Extensions;
using FileManagement.Core.Models;
using FileManagement.Core.Services;
using FileManagement.Data.Configurations;
using FileManagement.Data.Extensions;
using FileManagement.Firebase.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

builder.Services.AddServices();
builder.Services.AddRepositories();
builder.Services.AddFirebaseStorage(builder.Configuration);

builder.Services.AddAutoMapper(typeof(Program));

var mongoDbOptionsSection = builder.Configuration.GetSection(MongoDBOptions.Name);
builder.Services.Configure<MongoDBOptions>(mongoDbOptionsSection);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.RoutePrefix = "";
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "File Management");
    });
}

app.MapGet("/images", async(
        IImageService imageService,
        IMapper mapper) =>
    {
        var images = await imageService.GetImagesAsync();

        var imageResources = mapper.Map<IEnumerable<ImageResource>>(images);

        return Results.Ok(imageResources);
    })
    .Produces(200)
    .WithName("GetImages");

app.MapPost("/images", async(
        HttpRequest request,
        IImageService imageService,
        IMapper mapper) =>
    {
        var form = await request.ReadFormAsync();

        var imageFile = form.Files.GetFile(nameof(UploadImageResource.Image));

        if (imageFile is null || imageFile.Length == 0)
        {
            return Results.BadRequest("Image is not provided");
        }

        var uploadImage = mapper.Map<UploadImage>(imageFile);

        var image = await imageService.UploadImageAsync(uploadImage);

        var imageResource = mapper.Map<ImageResource>(image);

        return Results.Created(imageResource.Url, imageResource);
    })
    .Accepts<UploadImageResource>("multipart/form-data")
    .Produces(201)
    .Produces(400)
    .WithName("AddImages");



app.UseCors(config =>
{
    config.AllowAnyOrigin();
});

app.MapControllers();

app.Run();