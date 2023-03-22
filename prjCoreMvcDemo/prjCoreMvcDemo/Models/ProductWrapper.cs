﻿namespace prjCoreMvcDemo.Models
{
    public class ProductWrapper
    {
        private TProduct _product;

        public TProduct Product { 
            get { return _product; } 
            set { _product = value; } }

        public ProductWrapper()
        {
            // 避免 _product 為 null
            _product = new TProduct();
        }


        public int FId
        {
            get { return _product.FId; }
            set { _product.FId = value; }
        }

        public string? FName
        {
            get { return _product.FName; }
            set { _product.FName = value; }
        }

        public int? FQty
        {
            get { return _product.FQty; }
            set { _product.FQty = value; }
        }

        public decimal? FCost
        {
            get { return _product.FCost; }
            set { _product.FCost = value; }
        }

        public decimal? FPrice
        {
            get { return _product.FPrice; }
            set { _product.FPrice = value; }
        }

        public string? FImagePath
        {
            get { return _product.FImagePath; }
            set { _product.FImagePath = value; }
        }

        public IFormFile photo { get; set; }


        // end of class ProductWrapper
    }
}
