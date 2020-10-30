using RedisProjectForCloud.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RedisProjectForCloud.Redis.Interface
{
    public interface ICacheService
    {
        Task<T> GetAsync<T>(string key, string keyField);
        T Get<T>(string key, string keyField);
        void Add(string key, object data);
        void Remove(string key);
        void Clear();
        bool Any(string key);
        Task<bool> Add(string key, string keyField, User user);
        Task AddList(string key, List<User> userList);
    }
}
