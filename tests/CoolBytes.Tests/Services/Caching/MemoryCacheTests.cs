﻿using System.Threading.Tasks;
using CoolBytes.Core.Abstractions;
using CoolBytes.Core.Domain;
using CoolBytes.Core.Utils;
using CoolBytes.Services.Caching;
using CoolBytes.Tests.Web;
using CoolBytes.Tests.Web.Features;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace CoolBytes.Tests.Services.Caching
{
    public class MemoryCacheTests : TestBase<TestContext>
    {
        public int CategoryId { get; set; }

        public MemoryCacheTests(TestContext testContext) : base(testContext)
        {
        }

        public override async Task InitializeAsync()
        {
            using (var context = TestContext.CreateNewContext())
            {
                var category = new Category("Test", 1, "Test description");
                context.Categories.Add(category);
                await context.SaveChangesAsync();
                CategoryId = category.Id;
            }
        }


        [Fact]
        public async Task MemoryCache_Uses_Cache_Second_Time()
        {
            var memoryCache = TestContext.CreateMemoryCacheService();
            var foundCategory = await memoryCache.GetOrAddAsync(() => CategoryFactory());

            Assert.Equal("Test", foundCategory.Name);

            var category = await RequestDbContext.Categories.FirstAsync(c => c.Id == CategoryId);
            category.UpdateName("Test again");
            RequestDbContext.Categories.Update(category);
            await RequestDbContext.SaveChangesAsync();

            foundCategory = await memoryCache.GetOrAddAsync(() => CategoryFactory());

            Assert.Equal("Test", foundCategory.Name);
        }

        [Fact]
        public async Task When_Cache_Header_Is_Found_And_User_Is_Present_Cache_Gets_Disabled()
        {
            var httpContextAccessor = TestContext.CreateHttpContextAccessor(h => h.Request.Headers.Add("X-CACHE-ENABLED", "1"));
            var userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(u => u.TryGetCurrentUserAsync()).ReturnsAsync(new SuccessResult<User>(null));
            var cachePolicy = new DefaultCachePolicy(httpContextAccessor, userServiceMock.Object);
            var memoryCache = TestContext.CreateMemoryCacheService(cachePolicy);

            var foundCategory = await memoryCache.GetOrAddAsync(() => CategoryFactory());
            Assert.Equal("Test", foundCategory.Name);

            var category = await RequestDbContext.Categories.FirstAsync(c => c.Id == CategoryId);
            category.UpdateName("Test again");
            RequestDbContext.Categories.Update(category);
            await RequestDbContext.SaveChangesAsync();

            foundCategory = await memoryCache.GetOrAddAsync(() => CategoryFactory());

            Assert.Equal("Test again", foundCategory.Name);
        }

        [Fact]
        public async Task When_Cache_Header_Is_Found_And_User_Is_NOT_Present_Cache_Keeps_Being_Enabled()
        {
            var httpContextAccessor = TestContext.CreateHttpContextAccessor(h => h.Request.Headers.Add("X-CACHE-ENABLED", "1"));
            var userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(u => u.TryGetCurrentUserAsync()).ReturnsAsync(new ErrorResult<User>(string.Empty));
            var cachePolicy = new DefaultCachePolicy(httpContextAccessor, userServiceMock.Object);
            var memoryCache = TestContext.CreateMemoryCacheService(cachePolicy);

            var foundCategory = await memoryCache.GetOrAddAsync(() => CategoryFactory());
            Assert.Equal("Test", foundCategory.Name);

            var category = await RequestDbContext.Categories.FirstAsync(c => c.Id == CategoryId);
            category.UpdateName("Test again");
            RequestDbContext.Categories.Update(category);
            await RequestDbContext.SaveChangesAsync();

            foundCategory = await memoryCache.GetOrAddAsync(() => CategoryFactory());

            Assert.Equal("Test", foundCategory.Name);
        }


        private async Task<Category> CategoryFactory()
        {
            using (var context = TestContext.CreateNewContext())
            {
                return await context.Categories.FirstAsync(c => c.Id == CategoryId);
            }
        }
    }
}
