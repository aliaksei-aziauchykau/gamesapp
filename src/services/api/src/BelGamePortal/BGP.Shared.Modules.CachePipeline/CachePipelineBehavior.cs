using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace BGP.Shared.Modules.CachePipeline;


//Q: IMemoryCache interface package is
//A: Microsoft.Extensions.Caching.Memory

//Q: How to install Microsoft.Extensions.Caching.Memory package?
//A: dotnet add package Microsoft.Extensions.Caching.Memory



public class CachePipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    private readonly IMemoryCache _memoryCache;

    public CachePipelineBehavior(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var cacheAttribute = request.GetType().GetCustomAttribute<CachePipelineSettingsAttribute>();
        if (cacheAttribute == null)
        {
            return await next();
        }

        var cacheKey = GenerateCacheKey(request);

        if (_memoryCache.TryGetValue(cacheKey, out TResponse cachedResponse))
        {
            return cachedResponse;
        }

        var response = await next();

        var cacheEntryOptions = new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(cacheAttribute.ExpirationInSeconds)
        };

        _memoryCache.Set(cacheKey, response, cacheEntryOptions);

        return response;
    }

    private string GenerateCacheKey(TRequest request)
    {
        var requestType = request.GetType();
        var properties = requestType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
        var keyValues = properties.Select(p => $"{p.Name}:{p.GetValue(request)}");
        var key = $"{requestType.FullName}:{string.Join(",", keyValues)}";
        return key;
    }
}
