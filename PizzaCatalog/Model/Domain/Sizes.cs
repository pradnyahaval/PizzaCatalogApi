namespace PizzaCatalog.WebApi.Model.Domain
{
    public class Sizes
    {
        public int Id {  get; set; }
        public string Name { get; set; } //small, medium, large
        public decimal PriceMultiplier {  get; set; } //1, 1.5, 2
    }
}
