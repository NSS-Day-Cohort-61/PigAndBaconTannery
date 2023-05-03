using System.Collections.Generic;

namespace PigAndBaconTannery.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int VendorId { get; set; }
        public Vendor Vendor { get; set; }
        public ProductDetail ProductDetail { get; set; }
        public List<int> CategoryIds { get; set; }


    }
}