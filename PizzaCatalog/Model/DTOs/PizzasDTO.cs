namespace PizzaCatalog.WebApi.Model.DTOs
{
    public class PizzasDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal BasePrice { get; set; }
        public bool IsVeg { get; set; }

        public string? Image {  get; set; }

        public List<PizzaToppingsDTO> Toppings { get; set; }
    }
}
