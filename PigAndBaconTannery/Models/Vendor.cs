﻿namespace PigAndBaconTannery.Models
{
    public class Vendor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public Product Product { get; set; }
    }
}
