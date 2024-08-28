using AutoMapper;
using Core.Interfaces;
using Core.Models.Domain;
using Core.Specifications;
using market_api.DTOs.Categories;
using market_api.DTOs.Characteristics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace market_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCharacteristicsController(IGenericRepository<ProductCharacteristic> repository, IGenericRepository<Product> products) : ControllerBase
    {
        private readonly IGenericRepository<ProductCharacteristic> _repository = repository;
        private readonly IGenericRepository<Product> _products = products;

        [HttpPost]
        public async Task<ActionResult<BaseServiceResponse<GetProductCharacteristicRequestDto>>> CreateCharacteristic([FromBody] CreateOrUpdateProductCharacteristicRequestDto createCharacteristic)
        {
            var response = new BaseServiceResponse<GetProductCharacteristicRequestDto>();

            if (!_products.Exists(createCharacteristic.ProductId))
            {
                response.IsSuccess = false;
                response.Message = $"Product with ID {createCharacteristic.ProductId} doesn't exists!";

                return BadRequest(response);
            }

            var newCharacteristic = new ProductCharacteristic
            {
                Name = createCharacteristic.ProductCharacteristicName,
                Description = createCharacteristic.ProductCharacteristicDescription,
                Product = await _products.GetByIdAsync(createCharacteristic.ProductId)
            };

            if (newCharacteristic is null || !ModelState.IsValid)
            {
                response.IsSuccess = false;

                return BadRequest(response);
            }

            try
            {
                _repository.Add(newCharacteristic);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;

                return BadRequest(response);
            }

            if (await _repository.SaveAllChangesAsync())
            {
                response.Data = new GetProductCharacteristicRequestDto
                {
                    CharacteristicId = newCharacteristic.Id,
                    ProductCharacteristicName = newCharacteristic.Name,
                    ProductCharacteristicDescription = newCharacteristic.Description,
                    ProductId = newCharacteristic.Product!.Id
                };
                response.Message = "New characteristic created successfully";

                return Ok(response);
            }

            response.IsSuccess = false;
            response.Message = "Problem with creation new characteristic";

            return BadRequest(response);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<BaseServiceResponse<GetProductCharacteristicRequestDto>>> GetCharacteristicById([FromRoute] int id)
        {
            var response = new BaseServiceResponse<GetProductCharacteristicRequestDto>();
            var spec = new ProductCharacteristicSpecification(id);
            var characteristic = await _repository.GetEntityWithSpecs(spec);

            if (characteristic is null)
            {
                response.IsSuccess = false;
                response.Message = $"Characteristic with ID {id} not found!";

                return NotFound(response);
            }

            else response.Data = new GetProductCharacteristicRequestDto
                {
                    CharacteristicId = characteristic.Id,
                    ProductCharacteristicName = characteristic.Name,
                    ProductCharacteristicDescription = characteristic.Description,
                    ProductId = characteristic.Product!.Id
                };

            return Ok(response);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<BaseServiceResponse<GetProductCharacteristicRequestDto>>> UpdateCharacteristicById(int id, CreateOrUpdateProductCharacteristicRequestDto updateCharacteristic)
        {
            var response = new BaseServiceResponse<GetProductCharacteristicRequestDto>();
            var spec = new ProductCharacteristicSpecification(id);
            var characteristic = await _repository.GetEntityWithSpecs(spec);

            if (characteristic is null)
            {
                response.IsSuccess = false;
                response.Message = $"Characteristic with ID {id} doesn't exists!";

                return BadRequest(response);
            }

            characteristic.Name = updateCharacteristic.ProductCharacteristicName;
            characteristic.Description = updateCharacteristic.ProductCharacteristicDescription;

            if (_products.Exists(updateCharacteristic.ProductId)) characteristic.Product = null;

            else
            {
                response.IsSuccess = false;
                response.Message = $"Product with ID {updateCharacteristic.ProductId} doesn't exists!";

                return BadRequest(response);
            }

            try
            {
                _repository.Update(characteristic);
                
                await _repository.SaveAllChangesAsync();
                characteristic.Product = await _products.GetByIdAsync(updateCharacteristic.ProductId);

                _repository.Update(characteristic);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;

                return BadRequest(response);
            }

            if (await _repository.SaveAllChangesAsync())
            {
                response.Data = new GetProductCharacteristicRequestDto
                {
                    CharacteristicId = characteristic.Id,
                    ProductCharacteristicName = characteristic.Name,
                    ProductCharacteristicDescription = characteristic.Description,
                    ProductId = characteristic.Product!.Id
                };
                response.Message = $"Characteristic with ID {id} has been updated successfully!";

                return Ok(response);
            }

            response.IsSuccess = false;
            response.Message = $"Problem changing characteristic with ID {id}";

            return BadRequest(response);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<BaseServiceResponse<GetProductCharacteristicRequestDto>>> DeleteCharacteristicById(int id)
        {
            var response = new BaseServiceResponse<GetProductCharacteristicRequestDto>();
            var characteristic = await _repository.GetByIdAsync(id);

            if (characteristic is null)
            {
                response.IsSuccess = false;
                response.Message = $"Characteristic with ID {id} doesn't exist!";

                return NotFound(response);
            }

            try
            {
                _repository.Remove(characteristic);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;

                return BadRequest(response);
            }

            if (await _repository.SaveAllChangesAsync())
            {
                response.Message = $"Characteristic with ID {id} has been deleted successfully!";

                return Ok(response);
            }

            response.IsSuccess = false;
            response.Message = $"Problem deleting characteristic with ID {id}";

            return BadRequest(response);
        }
    }
}
