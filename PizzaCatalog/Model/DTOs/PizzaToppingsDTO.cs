using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace PizzaCatalog.WebApi.Model.DTOs
{
    public class PizzaToppingsDTO
    {
        [BindNever]
        public int Id { get; set; }
        public int PizzId { get; set; }
        public int ToppingId { get; set; }

    }
}
