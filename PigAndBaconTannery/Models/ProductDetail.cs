namespace PigAndBaconTannery.Models
{
    public class ProductDetail
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal? Weight { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
