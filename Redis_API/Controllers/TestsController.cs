using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Redis_API.Models;
using Redis_API.Services;

namespace Redis_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestsController : ControllerBase
    {
        private readonly ICacheService _cacheService;

        public TestsController(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }
        [HttpGet("Create")]
        public async Task<IActionResult> Create()
        {
            var models=await _cacheService.GetAsync<List<TestModel>>("models") ?? new List<TestModel>();
            var model = new TestModel(Faker.Name.First(), Faker.Name.Last());
            models.Add(model);
            await _cacheService.SetAsync("models", models);
            return Ok(model);
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult>GetById(string id)
        {
            var models =await _cacheService.GetAsync<List<TestModel>>("models");
            if (models!=null)
            {
                try
                {
                    var guidId = new Guid(id);
                    var model = models.FirstOrDefault(p => p.Id == guidId);
                    if (model !=null)
                    {
                        return Ok(model);
                    }
                }
                catch (Exception)
                {
                }
            }
            return BadRequest("Üye bulunamadı");
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var cache = await _cacheService.GetAsync<List<TestModel>>("models");
            return Ok(cache);
        }
    }
}
