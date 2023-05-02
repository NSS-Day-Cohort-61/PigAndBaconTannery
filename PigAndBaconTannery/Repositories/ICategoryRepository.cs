using System.Collections.Generic;
using PigAndBaconTannery.Models;

namespace PigAndBaconTannery.Repositories
{
    public interface ICategoryRepository
    {
        List<Category> GetAll();
    }
}