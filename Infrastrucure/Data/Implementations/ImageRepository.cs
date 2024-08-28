﻿using Core.Interfaces;
using Core.Models.Domain;
using Infrastructure.Data.App;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Implementations
{
    public class ImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContext;
        private readonly ApplicationContext _context;

        public ImageRepository(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContext, ApplicationContext context)
        {
            _webHostEnvironment = webHostEnvironment;
            _httpContext = httpContext;
            _context = context;
        }
        public async Task<IEnumerable<ProductImage>> GetAllImages()
        {
            return await _context.images.ToListAsync();
        }

        public async Task<ProductImage> Upload(IFormFile file, ProductImage image)
        {
            var localPath = Path.Combine(_webHostEnvironment.ContentRootPath, "images", $"{image.Filename}{image.FileExtension}");

            using var stream = new FileStream(localPath, FileMode.Create);

            await file.CopyToAsync(stream);

            var httpRequest = _httpContext.HttpContext!.Request;
            var urlPath = $"{httpRequest.Scheme}://{httpRequest.Host}{httpRequest.PathBase}/images/{image.Filename}{image.FileExtension}";

            image.ImageUrl = urlPath;

            await _context.images.AddAsync(image);
            await _context.SaveChangesAsync();

            return image;
        }
    }
}
