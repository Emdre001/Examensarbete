using System;
using System.Collections.Generic;

namespace Models;

    public interface IProduct
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductType { get; set; }
        public string ProductDescription { get; set; }
        public int ProductPrice { get; set; }
        public int ProductRating { get; set; }

        public  IBrand Brand { get; set; }
        public  List<IColor> Colors { get; set; }
        public  List<ISize> Sizes { get; set; }
        public  List<IOrder> Orders { get; set; }
    }
