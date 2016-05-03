using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RjamSoft.Tamil.Grammar.Prosody.Infrastructure.Contract;
using ServiceStack.Caching;


namespace RjamSoft.Tamil.Grammar.Prosody.Infrastructure.Impl
{
    public class DistributedCachingService : IDistributedCachingService
    {
        private readonly ICacheClient _cacheClient;

        public DistributedCachingService(ICacheClient cacheClient)
        {
            _cacheClient = cacheClient;
        }


        public bool Remove(string key)
        {
            return _cacheClient.Remove(key);
        }

        public void RemoveAll(IEnumerable<string> keys)
        {
            _cacheClient.RemoveAll(keys);
        }

        public T Get<T>(string key)
        {
            return _cacheClient.Get<T>(key);
        }

        public long Increment(string key, uint amount)
        {
            return _cacheClient.Increment(key, amount);
        }

        public long Decrement(string key, uint amount)
        {
            return _cacheClient.Decrement(key, amount);
        }

        public bool Add<T>(string key, T value)
        {
            return _cacheClient.Add<T>(key, value);
        }

        public bool Set<T>(string key, T value)
        {
            return _cacheClient.Set<T>(key, value);
        }

        public bool Replace<T>(string key, T value)
        {
            return _cacheClient.Replace<T>(key, value);
        }

        public bool Add<T>(string key, T value, DateTime expiresAt)
        {
            return _cacheClient.Add<T>(key, value, expiresAt);
        }

        public bool Set<T>(string key, T value, DateTime expiresAt)
        {
            return _cacheClient.Set<T>(key, value, expiresAt);
        }

        public bool Replace<T>(string key, T value, DateTime expiresAt)
        {
            return _cacheClient.Replace<T>(key, value, expiresAt);
        }

        public bool Add<T>(string key, T value, TimeSpan expiresIn)
        {
            return _cacheClient.Add<T>(key, value, expiresIn);
        }

        public bool Set<T>(string key, T value, TimeSpan expiresIn)
        {
            return _cacheClient.Set<T>(key, value, expiresIn);
        }

        public bool Replace<T>(string key, T value, TimeSpan expiresIn)
        {
            return _cacheClient.Replace<T>(key, value, expiresIn);
        }

        public void FlushAll()
        {
            _cacheClient.FlushAll();
        }

        public IDictionary<string, T> GetAll<T>(IEnumerable<string> keys)
        {
            return _cacheClient.GetAll<T>(keys);
        }

        public void SetAll<T>(IDictionary<string, T> values)
        {
            _cacheClient.SetAll<T>(values);
        }

        public void Dispose()
        {
            _cacheClient.Dispose();
        }
    }
}
