using Core.Interfaces;
using Core.Models.Domain;
using market_api.DTOs.Categories;
using Microsoft.AspNetCore.Mvc;

namespace market_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController(IGenericRepository<Category> repository, ILogger<CategoriesController> logger) : ControllerBase
    {
        private readonly IGenericRepository<Category> _repository = repository;
        private readonly ILogger<CategoriesController> _logger = logger;

        [HttpGet]
        public async Task<ActionResult<BaseServiceResponse<IReadOnlyList<GetCategoryRequestDto>>>> GetCategories()
        {

            var response = new BaseServiceResponse<IReadOnlyList<GetCategoryRequestDto>>();
            var categories = await _repository.ListAllAsync();

            response.Data = categories.Select(c => new GetCategoryRequestDto
            {
                CategoryId = c.Id,
                CategoryName = c.Name,
                Url = c.UrlHandle,
                IsSubCategory = c.IsSubCategory
            }).ToList();

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<BaseServiceResponse<GetCategoryRequestDto>>> CreateCategory(CreateOrUpdateCategoryRequestDto createCategory)
        {
            var response = new BaseServiceResponse<GetCategoryRequestDto>();
            var newCategory = new Category
            {
                Name = createCategory.CategoryName,
                UrlHandle = createCategory.Url,
                IsSubCategory = createCategory.IsSubCategory
            };

            if (newCategory is null || !ModelState.IsValid)
            {
                response.IsSuccess = false;

                return BadRequest(response);
            }

            try
            {
                _repository.Add(newCategory);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;

                return BadRequest(response);
            }

            if (await _repository.SaveAllChangesAsync())
            {
                response.Data = new GetCategoryRequestDto
                {
                    CategoryId = newCategory.Id,
                    CategoryName = newCategory.Name,
                    Url = newCategory.UrlHandle,
                    IsSubCategory= newCategory.IsSubCategory
                };
                response.Message = "New category created successfully";

                return Ok(response);
            }

            response.IsSuccess = false;
            response.Message = "Problem with creation new category";

            return BadRequest(response);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<BaseServiceResponse<GetCategoryRequestDto>>> GetCategoryById([FromRoute] int id)
        {
            var response = new BaseServiceResponse<GetCategoryRequestDto>();
            var category = await _repository.GetByIdAsync(id);

            if (category is null)
            {
                response.IsSuccess = false;
                response.Message = $"Category with ID {id} not found!";

                return NotFound(response);
            }

            else response.Data = new GetCategoryRequestDto
            {
                CategoryId = category.Id,
                CategoryName = category.Name,
                Url = category.UrlHandle,
                IsSubCategory = category.IsSubCategory,
            };

            return Ok(response);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<BaseServiceResponse<GetCategoryRequestDto>>> UpdateCategoryById([FromRoute] int id, CreateOrUpdateCategoryRequestDto updateCategory)
        {
            var response = new BaseServiceResponse<GetCategoryRequestDto>();
            var category = await _repository.GetByIdAsync(id);

            if (category is null)
            {
                response.IsSuccess = false;
                response.Message = $"Category with ID {id} doesn't exists!";

                return BadRequest(response);
            }

            category.Name = updateCategory.CategoryName;
            category.UrlHandle = updateCategory.Url;
            category.IsSubCategory = updateCategory.IsSubCategory;

            try
            {
                _repository.Update(category);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;

                return BadRequest(response);
            }

            if (await _repository.SaveAllChangesAsync())
            {
                response.Data = new GetCategoryRequestDto
                {
                    CategoryId = category.Id,
                    CategoryName = category.Name,
                    Url = category.UrlHandle,
                    IsSubCategory = updateCategory.IsSubCategory
                };
                response.Message = $"Category with ID {id} has been updated successfully!";

                return Ok(response);
            }

            response.IsSuccess = false;
            response.Message = $"Problem changing category with ID {id}";

            return BadRequest(response);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<BaseServiceResponse<GetCategoryRequestDto>>> DeleteCategoryById([FromRoute] int id)
        {
            var response = new BaseServiceResponse<GetCategoryRequestDto>();
            var category = await _repository.GetByIdAsync(id);

            if (category is null)
            {
                response.IsSuccess = false;
                response.Message = $"Category with ID {id} doesn't exist!";

                return NotFound(response);
            }

            try
            {
                _repository.Remove(category);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;

                return BadRequest(response);
            }

            if (await _repository.SaveAllChangesAsync())
            {
                response.Message = $"Category with ID {id} has been deleted successfully!";

                return Ok(response);
            }

            response.IsSuccess = false;
            response.Message = $"Problem deleting category with ID {id}";

            return BadRequest(response);
        }

    }
}
