using Core.Interfaces;
using Core.Models.Domain;
using market_api.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace market_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository _image;

        public ImagesController(IImageRepository image)
        {
            _image = image;
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile file, [FromForm] string fileName, [FromForm] string title)
        {
            ValidateFileUpload(file);

            if (ModelState.IsValid)
            {
                var productImage = new ProductImage
                {
                    FileExtension = Path.GetExtension(file.FileName).ToLower(),
                    Filename = fileName,
                    Title = title,
                    DateCreated = DateTime.Now,
                };

                productImage = await _image.Upload(file, productImage);

                return Ok("Image upload successfully");
            }

            return BadRequest(ModelState);
        }

        private void ValidateFileUpload(IFormFile file)
        {
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png" };

            if (!allowedExtensions.Contains(Path.GetExtension(file.FileName).ToLower())) ModelState.AddModelError("file", "Unsupported file format");

            if (file.Length > 10485760) ModelState.AddModelError("file", "File size can't be more 10MB");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllImages()
        {
            var images = await _image.GetAllImages();
            var response = new List<ProductImageDto>();

            foreach (var image in images)
            {
                response.Add(new ProductImageDto
                {
                    Title = image.Title,
                    DateCreated = image.DateCreated,
                    FileExtension = image.FileExtension,
                    Filename = image.Filename,
                    ImageUrl = image.ImageUrl
                });
            }

            return Ok(response);
        }
    }
 }
