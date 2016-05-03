using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using RjamSoft.Tamil.Grammar.Prosody.Infrastructure.Contract;

namespace RjamSoft.Tamil.Grammar.Prosody.Infrastructure.Impl
{
    public class SharedResourceManager : ISharedResourceManager
    {
        private object _resourceManagerLock = new object();
        public IDistributedCachingService DistributedCachingService { get; set; }
        private Dictionary<string, bool> _isLoaded = new Dictionary<string, bool>();
        public SharedResourceManager(IDistributedCachingService distributedCachingService)
        {
            DistributedCachingService = distributedCachingService;
        }

        private Dictionary<string, string> LoadStringResourcesIntoCache(string language)
        {
            Dictionary<string, string> resourceStrings = null;
            bool found = false;
            if (!_isLoaded.TryGetValue(language, out found))
            {
                lock (_resourceManagerLock)
                {
                    var resourceSet =
                        Resources.ProsodyResources.ResourceManager.GetResourceSet(new CultureInfo(language),
                            true, true);
                    resourceStrings = resourceSet.Cast<DictionaryEntry>()
                        .ToDictionary(x => x.Key.ToString(),
                            x => x.Value.ToString());
                    DistributedCachingService.Set<Dictionary<string, string>>(language, resourceStrings);
                    _isLoaded[language] = true;
                }
            }
            return resourceStrings;
        }
        public Dictionary<string, string> GetAllStrings(string language)
        {
            Dictionary<string, string> resourceStrings = null;
            if((resourceStrings = DistributedCachingService.Get<Dictionary<string, string>>(language)) == null)
            {
                resourceStrings = LoadStringResourcesIntoCache(language);
            }
            return resourceStrings;
        }

        public string Get(string key, string language)
        {
            var resourceStrings = LoadStringResourcesIntoCache(language);

            return resourceStrings[key];
        }
    }
}
