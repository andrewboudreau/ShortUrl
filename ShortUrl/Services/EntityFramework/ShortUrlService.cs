using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

using ShortUrl.Models;

namespace ShortUrl.Services.EntityFramework
{
    public class ShortUrlService : DbContext, IShortUrlService
    {
        public ShortUrlService()
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<ShortUrlService>());
        }

        public DbSet<ShortenedUrl> ShortenedUrls;

        public DbSet<Visitor> Visitors;

        public async Task<int> CreateAsync(string url)
        {
            var shortUrl = Set<ShortenedUrl>().Create();
            shortUrl.Created = DateTime.UtcNow;
            shortUrl.Url = url;

            Set<ShortenedUrl>().Attach(shortUrl);

            await SaveChangesAsync();

            return shortUrl.Id;
        }

        public Task<List<ShortenedUrl>> RecentShortenedUrls()
        {
            return Set<ShortenedUrl>().OrderByDescending(x => x.Created).Take(100).ToListAsync();
        }

        public Task<ShortenedUrl> GetAsync(int id)
        {
            return Set<ShortenedUrl>().FindAsync(id);
        }
    }
}