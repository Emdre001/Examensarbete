using System.Collections.Generic;

namespace Models.DTO;

public class ProductImageDTO
{
        public Guid ProductId { get; set; }

        public string ImageUrl { get; set; }
        
        public Product Product { get; set; }
    
}