using System;

namespace Models
{
    public interface IProduct
    {
        public GUID ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductType { get; set; }
        public string ProductDescription { get; set; }
        public int ProductStock { get; set; }
        public int ProductPrice { get; set; }
        public int ProductRating { get; set; }

    }
}