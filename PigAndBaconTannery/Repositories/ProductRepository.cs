using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using PigAndBaconTannery.Models;

namespace PigAndBaconTannery.Repositories
{
    public class ProductRepository : BaseRepository, IProductRepository
    {

        public ProductRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public List<Product> GetAll()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            SELECT p.Id as ProductId, p.[Name] as ProductName, p.Price, p.Quantity,
                                   v.Id as VendorId, v.[Name] as VendorName,
                                   pd.Id as ProductDetailId, pd.Description,pd.Weight,
                                   pc.Id as ProductCategoryId, pc.CategoryId as CategoryId    
                                FROM PRODUCT p
                                    LEFT JOIN Vendor v ON p.VendorId = v.Id
                                    LEFT JOIN ProductDetail pd ON p.Id = pd.ProductId
                                    LEFT JOIN ProductCategory pc ON p.Id = pc.ProductId";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var products = new List<Product>();
                        while (reader.Read())
                        {
                            var productId = reader.GetInt32(reader.GetOrdinal("ProductId"));
                            var existingProduct = products.FirstOrDefault(p => p.Id == productId) ?? new Product()
                            {
                                Id = productId,
                                Name = reader.GetString(reader.GetOrdinal("ProductName")),
                                Price = reader.GetDecimal(reader.GetOrdinal("Price")),
                                Quantity = reader.GetInt32(reader.GetOrdinal("Quantity")),
                                VendorId = reader.GetInt32(reader.GetOrdinal("VendorId")),
                                Vendor = new Vendor()
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("VendorId")),
                                    Name = reader.GetString(reader.GetOrdinal("VendorName"))
                                },
                                ProductDetail = new ProductDetail()
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("ProductDetailId")),
                                    Description = reader.GetString(reader.GetOrdinal("Description")),
                                    Weight = reader.GetInt32(reader.GetOrdinal("Weight"))
                                },
                                CategoryIds = new List<int>()
                            };

                            products.Add(existingProduct);

                            if (!reader.IsDBNull(reader.GetOrdinal("ProductCategoryId")))
                            {
                                existingProduct.CategoryIds.Add(reader.GetInt32(reader.GetOrdinal("CategoryId")));
                            }
                        }
                        return products;
                    }
                }
            }
        }


    }
}
