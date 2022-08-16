using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PizzaHome.Core.Entities;
using PizzaHome.Core.Interfaces;
using PizzaHome.ViewModels.Dtos;

namespace PizzaHome.Controllers
{
    [Authorize]
    [Route("api/shops")]
    [ApiController]
    public class ShopsController : ControllerBase
    {

        private IShopService _service;
        private IMapper _mapper;

        public ShopsController(IShopService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<ShopDto>>> Index() {

            var shops = await _service.GetAllShops();
            var mappedShops = _mapper.Map<List<ShopDto>>(shops);
            return Ok(mappedShops);
        }

        [HttpGet("{id}", Name = "GetShopById")]
        public async Task<ActionResult<ShopDto>> IndexById(int id)
        {

            var shop = await _service.GetShopById(id);
            var maapedShop = _mapper.Map<ShopDto>(shop);
            return Ok(maapedShop);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Shop shop) {

            var createdShop = await _service.CreateShop(shop);
            var mappedshop = _mapper.Map<ShopDto>(createdShop);
            return CreatedAtRoute("GetShopById", new { Id = mappedshop.Id}, mappedshop);
        }

        [HttpPut("{id}")]
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
            if (res == false) return NotFound();

            return NoContent();
        }


     
    }
}
