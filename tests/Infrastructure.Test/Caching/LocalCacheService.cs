using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging.Abstractions;

namespace Infrastructure.Test.Caching;

public class LocalCacheService : CacheService<FSH.Learn.Infrastructure.Caching.LocalCacheService>
{
    protected override FSH.Learn.Infrastructure.Caching.LocalCacheService CreateCacheService() =>
        new(
            new MemoryCache(new MemoryCacheOptions()),
            NullLogger<FSH.Learn.Infrastructure.Caching.LocalCacheService>.Instance);
}