using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using PigAndBaconTannery.Models;
using PigAndBaconTannery.Utils;

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
                                Quantity = DbUtils.GetNullableInt(reader,"Quantity"),
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
                                    Weight = DbUtils.GetNullableInt(reader, "Weight")
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

        public void Add(Product product)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    //Insert Product
                    cmd.CommandText = @"INSERT INTO Product (
                                            Name,
                                            Price,
                                            VendorId,
                                            Quantity)
                                        OUTPUT INSERTED.ID
                                        VALUES (@Name, @Price, @VendorId,@Quantity)";
                    cmd.Parameters.AddWithValue("@Name", product.Name);
                    cmd.Parameters.AddWithValue("@Price", product.Price);
                    cmd.Parameters.AddWithValue("@VendorId", product.VendorId);
                    cmd.Parameters.AddWithValue("@Quantity", product.Quantity);

                    product.Id = (int)cmd.ExecuteScalar();

                    //Insert Product Details
                    cmd.CommandText = @"INSERT INTO ProductDetail (ProductId, Description, Weight)
                                        OUTPUT INSERTED.ID
                                        VALUES (@ProductId, @Description, @Weight)";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@ProductId", product.Id);
                    cmd.Parameters.AddWithValue("@Description", product.ProductDetail.Description);
                    cmd.Parameters.AddWithValue("@Weight", product.ProductDetail.Weight);

                    cmd.ExecuteNonQuery();

                    //Insert Product Category
                    foreach (var c in product.CategoryIds)
                    {
                        cmd.CommandText = @"
                            INSERT INTO ProductCategory (ProductId, CategoryId)
                            VALUES(@ProductId, @CategoryId)";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@ProductId", product.Id);
                        cmd.Parameters.AddWithValue("@CategoryId", c);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
