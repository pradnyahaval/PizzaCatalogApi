using Microsoft.AspNetCore.Mvc;
using PizzaCatalog.WebApi.Model.DTOs;
using PizzaCatalog.WebApi.Repositories;

namespace PizzaCatalog.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToppingsController : ControllerBase
    {
        private readonly IToppingsRepository _toppingsRepository;

        public ToppingsController(IToppingsRepository toppingsRepository)
        {
            _toppingsRepository = toppingsRepository;
        }

        [HttpGet]
        [Route("GetAllToppings")]
        public async Task<IActionResult> GetAllToppings()
        {
            
            var toppingsDTOList = await _toppingsRepository.GetAllToppingsAsync();
                        
            if(toppingsDTOList != null)
            {
                return Ok(toppingsDTOList);
            }

            return NotFound();
        }

        [HttpGet]
        [Route("GetToppingsById/{id:int}")]
        public async Task<IActionResult> GetToppingsById(int id)
        {
            var toppingsDTO = await _toppingsRepository.GetToppingsByIdAsync(id);

            if (toppingsDTO != null)
            {
                return Ok(toppingsDTO);
            }

            return NotFound();           
        }

        [HttpPost]
        [Route("InsertToppings")]
        public IActionResult InsertToppings(ToppingsDTO toppingsDTO)
        {
            try
            {
                var toppingDTO = _toppingsRepository.InsertToppings(toppingsDTO);

                return new CreatedResult(nameof(InsertToppings), toppingDTO);
              
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
