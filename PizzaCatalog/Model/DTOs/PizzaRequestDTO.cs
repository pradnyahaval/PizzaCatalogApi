namespace PizzaCatalog.WebApi.Model.DTOs
{
    public class PizzaRequestDTO
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal BasePrice { get; set; }
        public bool IsVeg { get; set; }

        public string? Image { get; set; }

        public List<PizzaToppingsRequestDTO>? Toppings { get; set; }
    }

    public class PizzaToppingsRequestDTO
    {
        public int ToppingId { get; set; }
    }
}
