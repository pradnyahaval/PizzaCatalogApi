using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzaCatalog.WebApi.Data;
using PizzaCatalog.WebApi.Model.Domain;
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
        public async Task<IActionResult> GetAll()
        {            
            var pizzaDto = await _pizzasRepository.GetPizzasAsync();

            if (pizzaDto != null)
                return Ok(pizzaDto);

            return NotFound();
        }

        [HttpGet]
        [Route("{id:int}")]
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
        public async Task<IActionResult> InsertPizza(PizzaRequestDTO pizzasDTO)
        {

            var pizza = await _pizzasRepository.InsertPizzaAsync(pizzasDTO);

            return new CreatedResult(nameof(GetPizzaById), pizza);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeletePizza(int id)
        {
            await _pizzasRepository.DeletePizzaByIdAsync(id);

            return Ok();
        }
    }
}
