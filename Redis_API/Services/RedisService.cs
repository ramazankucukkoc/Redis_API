using Redis_API.Extensions;
using StackExchange.Redis;

namespace Redis_API.Services
{
    public class RedisService : ICacheService
    {
        private readonly ConnectionMultiplexer _multiplexer;
        public RedisService(IConfiguration configuration)
        {
            var connectionString = configuration.GetSection("RedisConfiguration:ConnectionString")?.Value;
            ConfigurationOptions options = new ConfigurationOptions
            {
                EndPoints = { connectionString },
                AbortOnConnectFail = false,
                AsyncTimeout = 10000,
                ConnectTimeout = 10000
            };
            _multiplexer = ConnectionMultiplexer.Connect(options);
        }
        public T Get<T>(string key) where T : class
        {
            string value =_multiplexer.GetDatabase().StringGet(key);
            return value.ToObject<T>();

        }

        public string Get(string key)
        {
            return _multiplexer.GetDatabase().StringGet(key);

        }

        public async Task<T> GetAsync<T>(string key) where T : class
        {
            string value = await _multiplexer.GetDatabase().StringGetAsync(key);
            return value.ToObject<T>();
        }

        public void Remove(string key)
        {
            _multiplexer.GetDatabase().KeyDelete(key);
        }

        public void Set(string key, string value)
        {
            _multiplexer.GetDatabase().StringSet(key, value);
        }

        public void Set<T>(string key, object value) where T : class
        {
            _multiplexer.GetDatabase().StringSet(key, value.ToJson());
        }

        public void Set(string key, object value, TimeSpan expiration)
        {
            _multiplexer.GetDatabase().StringSet(key, value.ToJson(), expiration);

        }

        public Task SetAsync(string key, object value)
        {
            return _multiplexer.GetDatabase().StringSetAsync(key, value.ToJson());
        }

        public Task SetAsync(string key, object value, TimeSpan expiration)
        {
            return Task.FromResult(_multiplexer.GetDatabase().StringSetAsync(key, value.ToJson(), expiration));
        }
    }
}
