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
    public class SharedResourceManagerTests
    {
        public IRedisClientsManager ClientsManager;
        public IDistributedCachingService DistributedCachingService;
        public ISharedResourceManager SharedResourceManager;

        [Fact]
        public void TestGetAllStrings()
        {
            ClientsManager = new PooledRedisClientManager(ConfigurationManager.AppSettings["RedisUri"]);
            DistributedCachingService = new DistributedCachingService(ClientsManager.GetCacheClient());
            SharedResourceManager = new SharedResourceManager(DistributedCachingService);

            var download = SharedResourceManager.Get("Download", "ta");

            Assert.Equal(download, "பதிவிறக்கம்");
        }
    }
}
