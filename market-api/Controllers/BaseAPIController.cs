using Azure;
using Core.Interfaces;
using Core.Models.Domain;
using market_api.DTOs.Products;
using market_api.RequestHelpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace market_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseAPIController : ControllerBase
    {
        protected async Task<ActionResult> CreatePagedResult<T>(IGenericRepository<T> repository, ISpecification<T> spec, int pageIndex, int pageSize) where T : BaseSimpleEntity
        {
            var items = await repository.ListAsyncWithSpecs(spec);
            var count = await repository.CountAsync(spec);
            var page = new Pagination<T>(pageIndex, pageSize, count, items);

            return Ok(page);
        }
    }
}
