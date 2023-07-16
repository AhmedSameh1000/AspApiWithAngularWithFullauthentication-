using System.ComponentModel.DataAnnotations.Schema;

namespace JWTApi.ViewModels
{
    public class ProductVM
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public int CategoryId { get; set; }
        public int BrandId { get; set; }

        [NotMapped]
        public IFormFile? File { get; set; }
    }
}