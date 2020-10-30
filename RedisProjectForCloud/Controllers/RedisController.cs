using Microsoft.AspNetCore.Mvc;
using RedisProjectForCloud.Model;
using RedisProjectForCloud.Redis.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RedisProjectForCloud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RedisController : ControllerBase
    {
        private static List<User> _users;
        private ICacheService _cacheService;

        public RedisController(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

        public IActionResult Index()
        {
            return Ok(_users);
        }

        [HttpGet]
        [Route("LoadAllData")]
        public async Task LoadAllData()
        {
            LoadUserList();
            await _cacheService.AddList("Users", _users);
        }

        [HttpGet]
        [Route("GetUserByIdAsync/{id}")]
        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _cacheService.GetAsync<User>("Users", $"user{id}").ConfigureAwait(false);
        }

        [HttpGet]
        [Route("GetUserByIdSync/{id}")]
        public User GetUserById(int id)
        {
            return _cacheService.Get<User>("Users", $"user{id}");
        }

        [HttpPost]
        [Route("AddUser")]
        public async Task<IActionResult> Add(User user)
        {
            await _cacheService.Add("Users", $"user{user.Id}", user);

            return Ok("User successfully added.");
        }

        private void LoadUserList()
        {
            if (_users == null)
            {
                _users = new List<User>
                {
                    new User(1, "Mustafa1", "Çakıroğlu1", "@hepsiburada", "www.hepsiburada.com"),
                    new User(2, "Mustafa2", "Çakıroğlu2", "@hepsiburada", "www.hepsiburada.com"),
                    new User(3, "Mustafa3", "Çakıroğlu3", "@hepsiburada", "www.hepsiburada.com"),
                    new User(4, "Mustafa4", "Çakıroğlu4", "@hepsiburada", "www.hepsiburada.com"),
                    new User(5, "Mustafa5", "Çakıroğlu5", "@hepsiburada", "www.hepsiburada.com")
                };
            }
        }
    }
}

