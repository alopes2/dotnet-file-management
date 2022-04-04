using AutoMapper;
using FileManagement.API.Models;
using FileManagement.Business.Extensions;
using FileManagement.Core.Models;
using FileManagement.Core.Services;
using FileManagement.Data.Configurations;
using FileManagement.Data.Extensions;
using FileManagement.Firebase.Configurations;
using FileManagement.Firebase.Extensions;
using Microsoft.AspNetCore.Mvc;

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
    app.UseSwaggerUI(options => {
        options.RoutePrefix = "";
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "File Management");
    });
}

app.MapGet("/images", async (
    IImageService imageService,
    IMapper mapper) =>
{
    var images = await imageService.GetImagesAsync();

    var imageResources = mapper.Map<IEnumerable<ImageResource>>(images);

    return Results.Ok(imageResources);
})
.WithName("GetImages");

app.UseCors(config => {
    config.AllowAnyOrigin();
});

app.MapControllers();

app.Run();