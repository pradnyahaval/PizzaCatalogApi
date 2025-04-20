namespace PizzaCatalog.WebApi.Model.Domain
{
    public class PizzaToppings
    {
        public int Id { get; set; }
        public int PizzId {  get; set; }
        public Pizzas Pizzas { get; set; }
        public int ToppingId {  get; set; }
        public Toppings Toppings { get; set; }
        public bool IsDefault_Topping { get; set; }

    }
}
