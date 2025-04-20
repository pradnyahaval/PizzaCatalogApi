namespace PizzaCatalog.WebApi.Model.Domain
{
    public class Pizzas
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal BasePrice { get; set; }
        public bool IsVeg {  get; set; }

        public PizzaImages PizzaImages { get; set; }
        public ICollection<PizzaToppings> PizzaToppings { get; set; } = new List<PizzaToppings>();
    }
}
