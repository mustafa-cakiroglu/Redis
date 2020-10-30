using Newtonsoft.Json;
using RedisProjectForCloud.Model;
using RedisProjectForCloud.Redis.Interface;
using StackExchange.Redis;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RedisProjectForCloud.Redis
{
    public class RedisCacheService : ICacheService
    {
        private RedisServer _redisServer;

        public RedisCacheService(RedisServer redisServer)
        {
            _redisServer = redisServer;
        }

        public void Add(string key, object data)
        {
            string jsonData = JsonConvert.SerializeObject(data);

            _redisServer.Database.SetAdd(key, jsonData);
        }

        public async Task<bool> Add(string key, string keyField, User user)
        {
            var fieldValue = _redisServer.Database.HashGet(key, keyField);

            if (!fieldValue.HasValue)
            {
                bool result = await _redisServer.Database.HashSetAsync(
                         "Users",
                         $"user{user.Id}",
                         JsonConvert.SerializeObject(user));

                return await Task.FromResult(result);
            }

            return await Task.FromResult(false);
        }

        public async Task AddList(string key, List<User> userList)
        {
            foreach (var user in userList)
            {
                await _redisServer.Database.HashSetAsync(
                    "Users",
                    $"user{user.Id}",
                    JsonConvert.SerializeObject(user));
            }
        }

        public async Task<T> GetAsync<T>(string key, string keyField)
        {
            var userModel = await _redisServer.Database.HashGetAsync(key, keyField);

            if (userModel != RedisValue.Null)
            {
                return JsonConvert.DeserializeObject<T>(userModel);
            }

            return default;
        }

        public T Get<T>(string key, string keyField)
        {
            var userModel = _redisServer.Database.HashGet(key, keyField);

            if (userModel != RedisValue.Null)
            {
                return JsonConvert.DeserializeObject<T>(userModel);
            }

            return default;
        }

        public void Remove(string key)
        {
            _redisServer.Database.KeyDelete(key);
        }

        public void Clear()
        {
            _redisServer.FlushDatabase();
        }

        public bool Any(string key)
        {
            throw new System.NotImplementedException();
        }
    }
}
