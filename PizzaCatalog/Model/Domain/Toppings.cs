namespace PizzaCatalog.WebApi.Model.Domain
{
    public class Toppings
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public ICollection<PizzaToppings> PizzaToppings { get; set; } = new List<PizzaToppings>();
    }
}
