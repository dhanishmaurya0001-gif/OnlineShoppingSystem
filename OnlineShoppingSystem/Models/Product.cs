using System.ComponentModel.DataAnnotations;

namespace OnlineShoppingSystem.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [StringLength(120)]
        public string Name { get; set; } = string.Empty;

        [Required, StringLength(2000)]
        public string Description { get; set; } = string.Empty;

        [Range(0.01, 1000000)]
        public decimal Price { get; set; }

        public string? ImageUrl { get; set; }

        public string? Category { get; set; }
    }
}