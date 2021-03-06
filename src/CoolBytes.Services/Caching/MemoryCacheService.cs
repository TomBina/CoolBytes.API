﻿using System;
using System.Collections.Concurrent;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CoolBytes.Core.Attributes;
using CoolBytes.Core.Utils;

namespace CoolBytes.Services.Caching
{
    [Inject(typeof(ICacheService))]
    public class MemoryCacheService : ICacheService
    {
        private static readonly ConcurrentDictionary<string, object> Store = new ConcurrentDictionary<string, object>();
        private readonly ICachePolicy _cachePolicy;
        private readonly ICacheKeyGenerator _cacheKeyGenerator;

        public MemoryCacheService(ICachePolicy cachePolicy, ICacheKeyGenerator cacheKeyGenerator)
        {
            _cachePolicy = cachePolicy;
            _cacheKeyGenerator = cacheKeyGenerator;
        }

        public async Task<T> GetOrAddAsync<T>(Expression<Func<Task<T>>> factoryExpression, params object[] arguments)
        {
            var cacheActive = await _cachePolicy.IsCacheActiveAsync();

            if (!cacheActive)
                return await factoryExpression.Compile()();

            var key = GenerateKey(factoryExpression, arguments);
            var value = await GetAsync<T>(key);

            if (value != null)
                return value;

            await AddAsync(key, factoryExpression.Compile());
            value = await GetAsync<T>(key);

            return value;
        }

        private string GenerateKey<T>(Expression<Func<Task<T>>> factoryExpression, object[] arguments)
        {
            var key = _cacheKeyGenerator.GetKey(factoryExpression, arguments);
            return key;
        }

        public ValueTask<T> GetAsync<T>(string key)
        {
            var value = (T)Store.Get(key);

            return new ValueTask<T>(value);
        }

        public async ValueTask AddAsync<T>(string key, Func<Task<T>> factory)
        {
            if (Store.ContainsKey(key))
                return;

            var entry = await factory();
            Store.TryAdd(key, entry);
        }

        public ValueTask RemoveAllAsync()
        {
            Store.Clear();

            return new ValueTask();
        }
    }
}