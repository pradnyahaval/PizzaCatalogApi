using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzaCatalog.WebApi.Data;
using PizzaCatalog.WebApi.Model.DTOs;

namespace PizzaCatalog.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzaCatalogController : ControllerBase
    {
        private readonly PizzaCatalogDBContext _DBContext;
        public PizzaCatalogController(PizzaCatalogDBContext pizzaCatalogDBContext)
        {
            _DBContext = pizzaCatalogDBContext;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var pizzaDto = _DBContext.Pizzas
                .Include(p => p.PizzaToppings)
                .ThenInclude(pt => pt.Toppings)
                .Include(p => p.PizzaImages)
                .Select(p => new PizzasDTO()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    BasePrice = p.BasePrice,
                    IsVeg = p.IsVeg,
                    Image = p.PizzaImages.PizzaImageUrl,
                    Toppings = p.PizzaToppings.Select(pt => new PizzaToppingsDTO()
                    {
                        Id = pt.Toppings.Id,
                        Name = pt.Toppings.Name
                    }).ToList()
                }).ToList();

            if(pizzaDto != null)
            {
                return Ok(pizzaDto);
            }

            return NotFound();
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetPizzaById(int id)
        {
            
            var pizza = _DBContext.Pizzas
                .Include(pt => pt.PizzaToppings)
                .ThenInclude(t => t.Toppings)
                .Include(pi => pi.PizzaImages)
                .Select(p =>  new PizzasDTO()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    BasePrice = p.BasePrice,
                    IsVeg = p.IsVeg,
                    Image = p.PizzaImages.PizzaImageUrl,
                    Toppings = p.PizzaToppings.Select( pt => new PizzaToppingsDTO()
                    {
                        Id = pt.Toppings.Id,
                        Name = pt.Toppings.Name

                    }).ToList()
                })
                .FirstOrDefault(x => x.Id == id);

            if(pizza != null)
            {               
                return Ok(pizza);
            }

            return NotFound();            
        }
    }
}
