using System;

namespace Models
{
    public class ProductImage
    {
        public Guid ImageId { get; set; } = Guid.NewGuid();
        public Guid ProductId { get; set; }

        public string ImageUrl { get; set; }
        
        public Product Product { get; set; }

    }
}
