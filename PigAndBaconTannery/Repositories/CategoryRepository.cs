using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using PigAndBaconTannery.Models;

namespace PigAndBaconTannery.Repositories
{
    public class CategoryRepository : BaseRepository, ICategoryRepository
    {
        public CategoryRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public List<Category> GetAll()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT c.Id as CategoryId, c.Name as CategoryName,
	                                        p.Id as ProductId, p.Name as ProductName, p.Price as ProductPrice, p.Quantity as ProductQuantity, p.VendorId as ProductVendorId,
	                                        pd.Id as ProductDetailId, pd.Description as ProductDetailDescription, pd.Weight as ProductWeight
                                        FROM Category c
                                            LEFT JOIN  ProductCategory pc ON c.Id = pc.CategoryId
                                            LEFT JOIN Product p ON pc.ProductId = p.Id
                                            LEFT JOIN ProductDetail pd ON pd.ProductId = p.Id";
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var categories = new List<Category>();
                        while (reader.Read())
                        {
                            var categoryId = reader.GetInt32(reader.GetOrdinal("CategoryId"));
                            var existingCategory = categories.FirstOrDefault(c => c.Id == categoryId);
                            if (existingCategory == null)
                            {
                                existingCategory = new Category()
                                {
                                    Id = categoryId,
                                    Name = reader.GetString(reader.GetOrdinal("CategoryName")),
                                    Products = new List<Product>()
                                };
                                categories.Add(existingCategory);
                            }

                            //IsDBNull returns TRUE if the specified column is equivalent to System.DBNull; otherwise, FALSE.
                            if (!reader.IsDBNull(reader.GetOrdinal("ProductId")))
                            {
                                existingCategory.Products.Add(new Product()
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("ProductId")),
                                    Name = reader.GetString(reader.GetOrdinal("ProductName")),
                                    Price = reader.GetDecimal(reader.GetOrdinal("ProductPrice")),
                                    Quantity = reader.GetInt32(reader.GetOrdinal("ProductQuantity")),
                                    VendorId = reader.GetInt32(reader.GetOrdinal("ProductVendorId")),
                                });
                            }
                        }
                        return categories;
                    }
                }
            }
        }

    }
}
