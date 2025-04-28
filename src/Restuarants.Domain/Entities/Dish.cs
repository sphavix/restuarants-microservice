namespace Restuarants.Domain.Entities
{
    public class Dish
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int? Calories {  get; set; }
        public int RestuarantId { get; set; }
    }
}
