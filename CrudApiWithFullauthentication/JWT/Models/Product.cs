namespace JWTApi.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string? Iamge { get; set; }
        public double Quantity { get; set; }
        public Category Category { get; set; }

        public int CategoryId { get; set; }
        public Brand Brand { get; set; }

        public int BrandId { get; set; }
    }
}