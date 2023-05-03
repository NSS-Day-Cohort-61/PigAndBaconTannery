using System.Collections.Generic;
using PigAndBaconTannery.Models;

namespace PigAndBaconTannery.Repositories
{
    public interface IProductRepository
    {
        List<Product> GetAll();
    }
}