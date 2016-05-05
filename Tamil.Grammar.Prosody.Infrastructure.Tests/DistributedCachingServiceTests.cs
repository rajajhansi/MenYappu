using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RjamSoft.Tamil.Grammar.Prosody.Infrastructure.Contract;
using RjamSoft.Tamil.Grammar.Prosody.Infrastructure.Impl;
using ServiceStack.Redis;
using Xunit;

namespace Tamil.Grammar.Prosody.Infrastructure.Tests
{
    public class DistributedCachingServiceFixture : IDisposable
    {
        public List<IRedisClientsManager> ClientsManagers = new List<IRedisClientsManager>();
        public List<IDistributedCachingService> DistributedCachingServices = new List<IDistributedCachingService>();

        public DistributedCachingServiceFixture()
        {
            ClientsManagers.Add(new PooledRedisClientManager(ConfigurationManager.AppSettings["RedisUri"]));
            DistributedCachingServices.Add(new DistributedCachingService(ClientsManagers[0].GetCacheClient()));
            ClientsManagers.Add(new PooledRedisClientManager(ConfigurationManager.AppSettings["AzureRedisUri"]));
            DistributedCachingServices.Add(new DistributedCachingService(ClientsManagers[1].GetCacheClient()));
        }
        public void Dispose()
        {

        }
    }
    public class DistributedCachingServiceTests : IClassFixture<DistributedCachingServiceFixture>
    {
        readonly DistributedCachingServiceFixture _distributedCachingServiceFixture;

        public DistributedCachingServiceTests(DistributedCachingServiceFixture distributedCachingServiceFixture)
        {
            _distributedCachingServiceFixture = distributedCachingServiceFixture;
        }

        [Fact]
        public void TestClearDistributedCaches()
        {
            var keys = new [] { "en", "ta" };

            foreach (var distributedCachingService in _distributedCachingServiceFixture.DistributedCachingServices)
            {
                distributedCachingService.RemoveAll(keys);

                Assert.Equal(null, distributedCachingService.Get<Dictionary<string, string>>(keys[0]));
            }
        }
    }
}
