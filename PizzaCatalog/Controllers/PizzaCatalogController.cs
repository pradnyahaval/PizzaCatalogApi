using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PizzaCatalog.WebApi.Model.DTOs;
using PizzaCatalog.WebApi.Repositories;

namespace PizzaCatalog.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzaCatalogController : ControllerBase
    {
        private readonly IPizzasRepository _pizzasRepository;
        public PizzaCatalogController(IPizzasRepository pizzasRepository)
        {
            _pizzasRepository = pizzasRepository;
        }
          
        [HttpGet]
        [Route("GetAll")]
        [Authorize(Roles ="Reader,Writers")]
        public async Task<IActionResult> GetAll()
        {            
            var pizzaDto = await _pizzasRepository.GetPizzasAsync();

            if (pizzaDto != null)
                return Ok(pizzaDto);

            return NotFound();
        }

        [HttpGet]
        [Route("GetPizzaById/{id:int}")]
        [Authorize(Roles = "Reader,Writer")]
        public async Task<IActionResult> GetPizzaById(int id)
        {
            
            var pizza = await _pizzasRepository.GetPizzaByIdAsync(id);

            if(pizza != null)
            {               
                return Ok(pizza);
            }

            return NotFound();            
        }

        [HttpPost]
        [Route("InsertPizza")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> InsertPizza(PizzaRequestDTO pizzasDTO)
        {

            var pizza = await _pizzasRepository.InsertPizzaAsync(pizzasDTO);

            return new CreatedResult(nameof(GetPizzaById), pizza);
        }

        [HttpPut]
        [Route("UpdatePizza")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> UpdatePizza(int pizzaid, PizzaUpdateDTO pizzaDTO)
        {
            
            var pizza = await _pizzasRepository.UpdatePizzaAsync(pizzaid, pizzaDTO);

            return Ok();
        }

        [HttpDelete]
        [Route("DeletePizza/{id:int}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> DeletePizza(int id)
        {
            await _pizzasRepository.DeletePizzaByIdAsync(id);

            return Ok();
        }

        [HttpPost] // [HttpPost("AddTopping")]
        [Route("AddToppingsToPizza")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> AddToppingsToPizza(int pizzaId, int toppingId)
        {            
            if (pizzaId != 0 && toppingId != 0)
            {
                await _pizzasRepository.AddToppingsToPizzaAsync(pizzaId, toppingId);
                return Ok(); // new CreatedResult( pizzaId);
            }
            return NotFound();
        }
    }
}
