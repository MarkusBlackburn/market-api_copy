using AutoMapper;
using Core.Interfaces;
using Core.Models.Domain;
using Core.Specifications;
using market_api.DTOs;
using market_api.DTOs.Categories;
using market_api.DTOs.Characteristics;
using market_api.DTOs.Products;
using market_api.RequestHelpers;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.PortableExecutable;

namespace market_api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController(IGenericRepository<Product> products, IGenericRepository<Category> categories, IGenericRepository<ProductCharacteristic> characteristic, IGenericRepository<ProductImage> images) : BaseAPIController
{
    private readonly IGenericRepository<Product> _products = products;
    private readonly IGenericRepository<Category> _categories = categories;
    private readonly IGenericRepository<ProductCharacteristic> _characteristic = characteristic;
    private readonly IGenericRepository<ProductImage> _images = images;

    [HttpGet]
    public async Task<ActionResult<BaseServiceResponse<IReadOnlyList<GetProductRequestDto>>>> GetProducts([FromQuery] ProductSpecParams specParams)
    {
        var response = new BaseServiceResponse<IReadOnlyList<GetProductRequestDto>>();
        var spec = new ProductSpecification(specParams);
        var products = await _products.ListAsyncWithSpecs(spec);
        var count = await _products.CountAsync(spec);

        response.Data = products.Select(c => new GetProductRequestDto
        {
            ProductId = c.Id,
            ProductName = c.Name,
            ProductCode = c.InternalCode,
            ProductPrice = c.Price,
            ProductDiscount = c.Discount,
            PriceWithDiscount = c.PriceWithDiscount,
            IsAvailable = c.IsAvailable,
            QuantityInStock = c.QuantityInStock,
            ProductUrl = c.UrlHandle,
            ProductDescription = c.Description,
            Categories = c.Categories?.Select(x => new GetCategoryRequestDto
            {
                CategoryId = x.Id,
                CategoryName = x.Name,
                Url = x.UrlHandle,
                IsSubCategory = x.IsSubCategory
            }).ToList(),
            Characteristics = c.Characteristics?.Select(x => new GetProductCharacteristicRequestDto
            {
                CharacteristicId = x.Id,
                ProductCharacteristicName = x.Name,
                ProductCharacteristicDescription = x.Description,
                ProductId = x.Product.Id
            }).ToList(),
            Images = c.Images?.Select(x => new ProductImageDto
            {
                Filename = x.Filename,
                FileExtension = x.FileExtension,
                Title = x.Title,
                ImageUrl = x.ImageUrl,
                ProductId = x.Product.Id,
                DateCreated = x.DateCreated
            }).ToList(),
            
        }).ToList();
        
        var page = new Pagination<GetProductRequestDto>(specParams.PageIndex, specParams.PageSize, count, response.Data);

        return Ok(page);
    }

    [HttpPost]
    public async Task<ActionResult<BaseServiceResponse<GetProductRequestDto>>> CreateProduct([FromBody] CreateProductRequestDto createProduct)
    {
        var response = new BaseServiceResponse<GetProductRequestDto>();
        var newProduct = new Product
        {
            Name = createProduct.ProductName,
            InternalCode = createProduct.ProductCode,
            Price = createProduct.ProductPrice,
            Discount = createProduct.ProductDiscount,
            IsAvailable = createProduct.IsAvailable,
            QuantityInStock = createProduct.QuantityInStock,
            Description = createProduct.ProductDescription,
            UrlHandle = createProduct.ProductUrl,
            Categories = [],
            Characteristics = [],
            Images = []
        };

        if (createProduct.ProductDiscount > 0 ) newProduct.PriceWithDiscount = newProduct.Price - (Math.Round(newProduct.Price * (newProduct.Discount / 100m), 2));
        else newProduct.PriceWithDiscount = createProduct.ProductPrice;

        foreach (var categoryId in createProduct.Categories!)
        {
            var existCategory = await _categories.GetByIdAsync(categoryId);
            if (existCategory is not null) newProduct.Categories?.Add(existCategory);
        }

        foreach (var characteristicId in createProduct.Characteristics!)
        {
            var existCharacteristic = await _characteristic.GetByIdAsync(characteristicId);
            if (existCharacteristic is not null) newProduct.Characteristics?.Add(existCharacteristic);
        }

        foreach (var imageId in createProduct.Images!)
        {
            var existImage = await _images.GetByIdAsync(imageId);
            if (existImage is not null) newProduct.Images?.Add(existImage);
        }

        if (newProduct is null || !ModelState.IsValid)
        {
            response.IsSuccess = false;
            
            return BadRequest(response);
        }

        try
        {
            _products.Add(newProduct);
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = ex.Message;

            return BadRequest(response);
        }

        if (await _products.SaveAllChangesAsync())
        {
            response.Data = new GetProductRequestDto
            {
                ProductId = newProduct.Id,
                ProductName = newProduct.Name,
                ProductCode = newProduct.InternalCode,
                ProductPrice = newProduct.Price,
                ProductDiscount = newProduct.Discount,
                PriceWithDiscount = newProduct.PriceWithDiscount,
                IsAvailable = newProduct.IsAvailable,
                QuantityInStock = newProduct.QuantityInStock,
                ProductUrl = newProduct.UrlHandle,
                ProductDescription = newProduct.Description,
                Categories = newProduct.Categories?.Select(x => new GetCategoryRequestDto
                {
                    CategoryId = x.Id,
                    CategoryName = x.Name,
                    Url = x.UrlHandle,
                    IsSubCategory = x.IsSubCategory
                }).ToList(),
                Characteristics = newProduct.Characteristics?.Select(x => new GetProductCharacteristicRequestDto
                {
                    CharacteristicId = x.Id,
                    ProductCharacteristicName = x.Name,
                    ProductCharacteristicDescription = x.Description,
                    ProductId = x.Product!.Id
                }).ToList(),
                Images = newProduct.Images?.Select(x => new ProductImageDto
                {
                    Filename = x.Filename,
                    FileExtension = x.FileExtension,
                    Title = x.Title,
                    ImageUrl = x.ImageUrl,
                    ProductId = x.Product!.Id,
                    DateCreated = x.DateCreated
                }).ToList(),
            };

            response.Message = "New product created successfully";

            return Ok(response);
        }

        response.IsSuccess = false;
        response.Message = "Problem with creation new product";

        return BadRequest(response);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<BaseServiceResponse<GetProductRequestDto>>> GetProductById([FromRoute] int id)
    {
        var response = new BaseServiceResponse<GetProductRequestDto>();
        var spec = new SingleProductSpecification(id);
        var product = await _products.GetEntityWithSpecs(spec);

        if (product is null)
        {
            response.IsSuccess = false;
            response.Message = $"Product with ID {id} not found!";

            return NotFound(response);
        }

        else response.Data = new GetProductRequestDto
        {
            ProductId = product.Id,
            ProductName = product.Name,
            ProductCode = product.InternalCode,
            ProductPrice = product.Price,
            ProductDiscount = product.Discount,
            PriceWithDiscount = product.PriceWithDiscount,
            IsAvailable = product.IsAvailable,
            QuantityInStock = product.QuantityInStock,
            ProductUrl = product.UrlHandle,
            ProductDescription = product.Description,
            Categories = product.Categories?.Select(x => new GetCategoryRequestDto
            {
                CategoryId = x.Id,
                CategoryName = x.Name,
                Url = x.UrlHandle,
                IsSubCategory = x.IsSubCategory
            }).ToList(),
            Characteristics = product.Characteristics?.Select(x => new GetProductCharacteristicRequestDto
            {
                CharacteristicId = x.Id,
                ProductCharacteristicName = x.Name,
                ProductCharacteristicDescription = x.Description,
                ProductId = x.Product!.Id
            }).ToList(),
            Images = product.Images?.Select(x => new ProductImageDto
            {
                Filename = x.Filename,
                FileExtension = x.FileExtension,
                Title = x.Title,
                ImageUrl = x.ImageUrl,
                ProductId = x.Product!.Id,
                DateCreated = x.DateCreated
            }).ToList(),

        };

        return Ok(response);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<BaseServiceResponse<GetProductRequestDto>>> UpdateProductById(int id, UpdateProductRequestDto updateProduct)
    {
        var response = new BaseServiceResponse<GetProductRequestDto>();
        var product = await _products.GetByIdAsync(id);
        
        if (product is null)
        {
            response.IsSuccess = false;
            response.Message = $"Product with ID {id} doesn't exists!";

            return BadRequest(response);
        }

        product.Name = updateProduct.ProductName;
        product.InternalCode = updateProduct.ProductCode;
        product.Price = updateProduct.ProductPrice;
        product.Discount = updateProduct.ProductDiscount;

        if (updateProduct.ProductDiscount > 0) product.PriceWithDiscount = product.Price - (Math.Round(product.Price * (product.Discount / 100m), 2));
        else product.PriceWithDiscount = updateProduct.ProductPrice;

        product.IsAvailable = updateProduct.IsAvailable;
        product.QuantityInStock = updateProduct.QuantityInStock;
        product.Description = updateProduct.ProductDescription;
        product.UrlHandle = updateProduct.ProductUrl;
        product.Categories = [];
        product.Characteristics = [];
        product.Images = [];

        foreach (var categoryId in updateProduct.Categories!)
        {
            var existCategory = await _categories.GetByIdAsync(categoryId);
            if (existCategory is not null) product.Categories?.Add(existCategory);
        }

        foreach (var characteristicId in updateProduct.Characteristics!)
        {
            var existCharacteristic = await _characteristic.GetByIdAsync(characteristicId);
            if (existCharacteristic is not null) product.Characteristics?.Add(existCharacteristic);
        }

        foreach (var imageId in updateProduct.Images!)
        {
            var existImage = await _images.GetByIdAsync(imageId);
            if (existImage is not null) product.Images?.Add(existImage);
        }

        try
        {
            _products.Update(product);
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = ex.Message;

            return BadRequest(response);
        }

        if (await _products.SaveAllChangesAsync())
        {
            response.Data = new GetProductRequestDto
            {
                ProductId = product.Id,
                ProductName = product.Name,
                ProductCode = product.InternalCode,
                ProductPrice = product.Price,
                ProductDiscount = product.Discount,
                PriceWithDiscount = product.PriceWithDiscount,
                IsAvailable = product.IsAvailable,
                QuantityInStock = product.QuantityInStock,
                ProductUrl = product.UrlHandle,
                ProductDescription = product.Description,
                Categories = product.Categories?.Select(x => new GetCategoryRequestDto
                {
                    CategoryId = x.Id,
                    CategoryName = x.Name,
                    Url = x.UrlHandle,
                    IsSubCategory = x.IsSubCategory
                }).ToList(),
                Characteristics = product.Characteristics?.Select(x => new GetProductCharacteristicRequestDto
                {
                    CharacteristicId = x.Id,
                    ProductCharacteristicName = x.Name,
                    ProductCharacteristicDescription = x.Description,
                    ProductId = x.Product!.Id
                }).ToList(),
                Images = product.Images?.Select(x => new ProductImageDto
                {
                    Filename = x.Filename,
                    FileExtension = x.FileExtension,
                    Title = x.Title,
                    ImageUrl = x.ImageUrl,
                    ProductId = x.Product!.Id,
                    DateCreated = x.DateCreated
                }).ToList(),

            };

            response.Message = $"Product with ID {id} has been updated successfully!";
            
            return Ok(response);
        }

        response.IsSuccess = false;
        response.Message = $"Problem changing product with ID {id}";

        return BadRequest(response);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<BaseServiceResponse<GetProductRequestDto>>> DeleteProductById(int id)
    {
        var response = new BaseServiceResponse<GetProductRequestDto>();
        var product = await _products.GetByIdAsync(id);

        if (product is null)
        {
            response.IsSuccess = false;
            response.Message = $"Product with ID {id} doesn't exist!";

            return NotFound(response);
        }

        try
        {
            _products.Remove(product);
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = ex.Message;

            return BadRequest(response);
        }

        if (await _products.SaveAllChangesAsync())
        {
            response.Message = $"Product with ID {id} has been deleted successfully!";

            return Ok(response);
        }

        response.IsSuccess = false;
        response.Message = $"Problem deleting product with ID {id}";

        return BadRequest(response);
    }
}