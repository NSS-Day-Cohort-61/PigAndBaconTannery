using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PigAndBaconTannery.Models;
using PigAndBaconTannery.Repositories;

namespace PigAndBaconTannery.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly IUserProfileRepository _userProfileRepository;
        public UserProfileController(IUserProfileRepository userProfileRepository)
        {
            _userProfileRepository = userProfileRepository;
        }

        //[HttpGet("{firebaseUserId}")]
        //public IActionResult GetByFirebaseUserId(string firebaseUserId)
        //{
        //    var userProfile = _userProfileRepository.GetByFirebaseUserId(firebaseUserId);
        //    if (userProfile == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(userProfile);
        //}

        [HttpGet("DoesUserExist/{firebaseUserId}")]
        public IActionResult DoesUserExist(string firebaseUserId)
        {
            var userProfile = _userProfileRepository.GetByFirebaseUserId(firebaseUserId);
            if (userProfile == null)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, UserProfile user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _userProfileRepository.Update(user);
            return NoContent();
        }

        [HttpPost]
        public IActionResult Register(UserProfile userProfile)
        {
            _userProfileRepository.Add(userProfile);
            return CreatedAtAction(
                "GetByFirebaseUserId", new { firebaseUserId = userProfile.FirebaseUserId }, userProfile);
        }
    }
}
