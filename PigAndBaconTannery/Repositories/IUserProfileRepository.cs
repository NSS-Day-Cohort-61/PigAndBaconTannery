using System.Collections.Generic;
using PigAndBaconTannery.Models;

namespace PigAndBaconTannery.Repositories
{
    public interface IUserProfileRepository
    {
        List<UserProfile> GetAll();
        UserProfile GetById(int id);
        UserProfile GetByFirebaseUserId(string id);
        void Add(UserProfile user);
        void Update(UserProfile user);
        void Delete(int id);
    }
}