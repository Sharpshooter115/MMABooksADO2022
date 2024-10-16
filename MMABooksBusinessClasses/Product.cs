using System;
using System.Collections.Generic;
using System.Text;

namespace MMABooksBusinessClasses
{
    public class Product
    {
        public string ProductCode { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
        public int OnHandQuantity { get; set; }

        public override string ToString()
        {
            return $"{ProductCode.Trim()}: {Description} - Price: {UnitPrice:C}, Quantity: {OnHandQuantity}";
        }
    }
}