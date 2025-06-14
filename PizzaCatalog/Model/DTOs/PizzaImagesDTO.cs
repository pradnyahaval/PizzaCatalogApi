using PizzaCatalog.WebApi.Model.Domain;

namespace PizzaCatalog.WebApi.Model.DTOs
{
    public class PizzaImagesDTO
    {
        public int Id { get; set; }
        public int PizzaId { get; set; }
        public string PizzaImageUrl { get; set; }
    }
}
