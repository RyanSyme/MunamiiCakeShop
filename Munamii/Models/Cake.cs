namespace Munamii.Models
{
    public class Cake
    {
        public int CakeId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsCakeOfTheWeek { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; } = default!;
    }
}
