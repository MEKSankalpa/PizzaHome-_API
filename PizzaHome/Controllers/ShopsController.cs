using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PizzaHome.Models;
using PizzaHome.Services.Interfaces;

namespace PizzaHome.Controllers
{
    [Route("api/shops")]
    [ApiController]
    public class ShopsController : ControllerBase
    {

        private IShopRepository _service;

        public ShopsController(IShopRepository repository)
        {
            _service = repository;
        }

        [HttpGet("{id?}", Name ="GetShopById")]
        public async Task<ActionResult<List<Shop>>> Index(int? id) {

            var result = await _service.GetAllShops();

            if (id is null) {
                return Ok(result);
            }

            result = result.Where(s => s.Id == id).ToList();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Shop shop) {

            var createdShop = await _service.CreateShop(shop);
            return CreatedAtRoute("GetShopById", new { Id = createdShop.Id}, createdShop);
        }

        [HttpPost("{id}")]
        public async Task<ActionResult> Update( int id, Shop shop) {
            if (id != shop.Id)
            {
                return BadRequest();
            }

            await _service.UpdateShop(id, shop);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id) {
            var res = await _service.DeleteShop(id);
            if (res == false)
            {
                return NotFound();
            }
            return NoContent();
        }


     
    }
}
