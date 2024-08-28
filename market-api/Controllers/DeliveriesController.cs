using Core.Interfaces;
using Core.Models.Domain;
using market_api.DTOs.Deliveries;
using Microsoft.AspNetCore.Mvc;

namespace market_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveriesController(IGenericRepository<DeliveryMethod> deliveries) : ControllerBase
    {
        private readonly IGenericRepository<DeliveryMethod> _deliveries = deliveries;

        [HttpGet("delivery-methods")]
        public async Task<ActionResult<BaseServiceResponse<IReadOnlyList<GetDeliveryMethodRequestDto>>>> GetDeliveryMethods()
        {
            var response = new BaseServiceResponse<IReadOnlyList<GetDeliveryMethodRequestDto>>();
            var deliveries = await _deliveries.ListAllAsync();

            response.Data = deliveries.Select(c => new GetDeliveryMethodRequestDto
            {
                DeliveryMethodId = c.Id,
                Name = c.ShortName,
                DeliveryTime = c.DeliveryTime,
                Description = c.Description,
                Price = c.Price
            }).ToList();

            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<BaseServiceResponse<GetDeliveryMethodRequestDto>>> GetDeliveryMethodById([FromRoute] int id)
        {
            var response = new BaseServiceResponse<GetDeliveryMethodRequestDto>();
            var delivery = await _deliveries.GetByIdAsync(id);

            if (delivery is null)
            {
                response.IsSuccess = false;
                response.Message = $"Delivery with ID {id} not found or doesn't exist!";

                return BadRequest(response);
            }

            response.Data = new GetDeliveryMethodRequestDto
            {
                DeliveryMethodId = delivery.Id,
                Name = delivery.ShortName,
                DeliveryTime = delivery.DeliveryTime,
                Description = delivery.Description,
                Price = delivery.Price
            };

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<BaseServiceResponse<GetDeliveryMethodRequestDto>>> CreateDeliveryMethod(CreateOrUpdateDeliveryMethodDto delivery)
        {
            var response = new BaseServiceResponse<GetDeliveryMethodRequestDto>();
            var newDelivery = new DeliveryMethod
            {
                ShortName = delivery.Name,
                DeliveryTime = delivery.DeliveryTime,
                Description = delivery.Description,
                Price = delivery.Price
            };

            if (delivery is null || !ModelState.IsValid)
            {
                response.IsSuccess = false;

                return BadRequest(response);
            }

            try
            {
                _deliveries.Add(newDelivery);
            }

            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;

                return BadRequest(response);
            }

            if (await _deliveries.SaveAllChangesAsync())
            {
                response.Data = new GetDeliveryMethodRequestDto
                {
                    DeliveryMethodId = newDelivery.Id,
                    DeliveryTime = delivery.DeliveryTime,
                    Description = delivery.Description,
                    Price = delivery.Price,
                    Name = delivery.Name
                };
                response.Message = "New delivery method has been created successfully";

                return Ok(response);
            }

            response.IsSuccess = false;
            response.Message = "Problem with creating new delivery method";

            return BadRequest(response);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<BaseServiceResponse<GetDeliveryMethodRequestDto>>> UpdateDeliveryMethodById([FromRoute] int id, CreateOrUpdateDeliveryMethodDto delivery)
        {
            var response = new BaseServiceResponse<GetDeliveryMethodRequestDto>();
            var exDelivery = await _deliveries.GetByIdAsync(id);

            if (exDelivery is null)
            {
                response.IsSuccess = false;
                response.Message = $"Delivery method with ID {id} not found or doesn't exist!";

                return BadRequest(response);
            }

            exDelivery.ShortName = delivery.Name;
            exDelivery.Description = delivery.Description;
            exDelivery.Price = delivery.Price;
            exDelivery.DeliveryTime = delivery.DeliveryTime;

            try
            {
                _deliveries.Update(exDelivery);
            }

            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;

                return BadRequest(response);
            }

            if (await _deliveries.SaveAllChangesAsync())
            {
                response.Data = new GetDeliveryMethodRequestDto
                {
                    DeliveryMethodId = exDelivery.Id,
                    Name = exDelivery.ShortName,
                    Description = exDelivery.Description,
                    Price = exDelivery.Price,
                    DeliveryTime = exDelivery.DeliveryTime,
                };
                response.Message = $"Delivery method with ID {id} has been updated successfully";

                return Ok(response);
            }

            response.IsSuccess = false;
            response.Message = $"Problem changing delivery method with ID {id}";

            return BadRequest(response);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<BaseServiceResponse<GetDeliveryMethodRequestDto>>> DeliteDeliveryMethodById([FromRoute] int id)
        {
            var response = new BaseServiceResponse<GetDeliveryMethodRequestDto>();
            var delivery = await _deliveries.GetByIdAsync(id);

            if (delivery is null)
            {
                response.IsSuccess = false;
                response.Message = $"Delivery method with ID {id} not found or doesn't exist!";

                return BadRequest(response);
            }

            try
            {
                _deliveries.Remove(delivery);
            }

            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;

                return BadRequest(response);
            }

            if (await _deliveries.SaveAllChangesAsync())
            {
                response.Message = $"Delivery method with ID {id} has been deleted successfully";

                return Ok(response);
            }

            response.IsSuccess = false;
            response.Message = $"Problem deleting delivery method with ID {id}";

            return BadRequest(response);
        }
    }
}
