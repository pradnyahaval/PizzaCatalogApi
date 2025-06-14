namespace PizzaCatalog.WebApi.Model.Domain
{
    public class PizzaImages
    {
        public int Id { get; set; }
        public int PizzaId { get; set; }
        public string PizzaImageUrl {  get; set; }

        public Pizzas Pizzas { get; set; }
    }
}
