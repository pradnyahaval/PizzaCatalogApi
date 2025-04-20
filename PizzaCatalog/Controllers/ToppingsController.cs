using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PizzaCatalog.WebApi.Data;
using PizzaCatalog.WebApi.Model.Domain;
using PizzaCatalog.WebApi.Model.DTOs;

namespace PizzaCatalog.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToppingsController : ControllerBase
    {
        private readonly PizzaCatalogDBContext _dbContext;

        public ToppingsController(PizzaCatalogDBContext pizzaCatalogDBContext)
        {
            _dbContext = pizzaCatalogDBContext;
        }

        [HttpGet]
        public IActionResult GetAllToppings()
        {
            var toppings = _dbContext.Toppings.ToList();

            var toppingsDTOList = new List<ToppingsDTO>();
            
            foreach (var topping in toppings)
            {
                var toppingDTO = new ToppingsDTO()
                {
                    Id = topping.Id,
                    Name = topping.Name,
                    Price = topping.Price
                };
                toppingsDTOList.Add(toppingDTO);
            }

            if(toppingsDTOList != null)
            {
                return Ok(toppingsDTOList);
            }

            return NotFound();
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetToppingsById(int id)
        {
            var toppings = _dbContext.Toppings.FirstOrDefault(x => x.Id == id);

            if (toppings != null)
            {
                var toppingsDTO = new ToppingsDTO()
                {
                    Id = toppings.Id,
                    Name = toppings.Name,
                    Price = toppings.Price
                };
                return Ok(toppingsDTO);
            }

            return NotFound();           
        }

        [HttpPost]
        public IActionResult InsertToppings(ToppingsDTO toppingsDTO)
        {
            try
            {
                var toppings = new Toppings()
                {
                    Name = toppingsDTO.Name,
                    Price = toppingsDTO.Price
                };

                _dbContext.Toppings.Add(toppings);
                _dbContext.SaveChanges();

                var toppingDTO = new ToppingsDTO()
                {
                    Id = toppings.Id,
                    Name = toppings.Name,
                    Price = toppings.Price
                };

                return new CreatedResult(nameof(GetToppingsById), toppingDTO);
              
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
