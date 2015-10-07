using System;
using System.Collections.Specialized;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

using ShortUrl.Models;

namespace ShortUrl.Services.EntityFramework
{
    /// <summary>
    /// EntityFramework 6 persistance
    /// </summary>
    public class ShortUrlContext : DbContext, IShortUrlService
    {
        static ShortUrlContext()
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<ShortUrlContext>());
        }

        public DbSet<ShortenedUrl> ShortenedUrls { get; set; }

        public DbSet<Visitor> Visitors { get; set; }

        #region IShortUrlService
        /// <summary>
        /// Shortens a url
        /// </summary>
        /// <param name="url">Url to shorten</param>
        /// <returns>The id of the shortened url</returns>
        public async Task<int> ShortenUrlAsync(string url)
        {
            var shortUrl = ShortenedUrls.Create();
            shortUrl.Created = DateTime.UtcNow;
            shortUrl.Url = url;

            ShortenedUrls.Add(shortUrl);

            await SaveChangesAsync();

            return shortUrl.Id;
        }

        /// <summary>
        /// 100 recent shortened urls
        /// </summary>
        /// <returns></returns>
        public IQueryable<ShortenedUrl> RecentShortenedUrls()
        {
            return Set<ShortenedUrl>().OrderByDescending(x => x.Created).Take(20);
        }

        public async Task AddVisitorAsync(int id, NameValueCollection headers, string userAgent)
        {
            var visitor = Visitors.Create();
            visitor.ShortUrlId = id;
            visitor.Agent = userAgent;
            visitor.Created = DateTime.UtcNow;
            visitor.Headers = headers.ToString();

            Visitors.Add(visitor);
            await SaveChangesAsync();
        }

        /// <summary>
        /// Shortened url details
        /// </summary>
        /// <param name="id">shortened url id</param>
        /// <returns>details of a specific shortened url</returns>
        public Task<ShortenedUrl> GetShortenedUrlAsync(int id)
        {
            return Set<ShortenedUrl>().FindAsync(id);
        }
        #endregion
    }
}