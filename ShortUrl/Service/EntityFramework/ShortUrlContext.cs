using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ShortUrl.Models;

namespace ShortUrl.Service.EntityFramework
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

        public ShortUrlContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<ShortenedUrl> ShortenedUrls { get; set; }

        public DbSet<Visitor> Visitors { get; set; }

        #region IShortUrlService

        public async Task DeleteUrlByIdAsync(int id)
        {
            var shortenedUrl = await Set<ShortenedUrl>().FindAsync(id);
            if (shortenedUrl != null)
            {
                Set<ShortenedUrl>().Remove(shortenedUrl);
                await SaveChangesAsync();
            }
        }

        /// <summary>
        /// Shortens a url
        /// </summary>
        /// <param name="url">Url to shorten</param>
        /// <returns>The id of the shortened url</returns>
        public async Task<int> ShortenUrlAsync(string url)
        {
            url = DefaultToHttpsProtocol(url);
            var uri = new Uri(url, UriKind.RelativeOrAbsolute);
            var request = new HttpClient().GetAsync(uri.ToString());

            var shortUrl = ShortenedUrls.Create();
            shortUrl.Created = DateTime.UtcNow;
            shortUrl.Url = uri.ToString();
            ShortenedUrls.Add(shortUrl);

            if (request.Wait(2000))
            {
                shortUrl.HttpStatusCode = (int)request.Result.StatusCode;
                if (request.Result.IsSuccessStatusCode)
                {
                    var response = await request.Result.Content.ReadAsStringAsync();
                    shortUrl.Title = GetTitle(response);
                }
            }

            await SaveChangesAsync();
            return shortUrl.Id;
        }

        /// <summary>
        /// 100 recent shortened urls
        /// </summary>
        /// <returns></returns>
        public Task<List<ShortenedUrl>> RecentShortenedUrls()
        {
            return Set<ShortenedUrl>().OrderByDescending(x => x.Id).Take(50).ToListAsync();
        }

        /// <summary>
        /// Add visitor data to a shortened url.
        /// </summary>
        /// <param name="id">Short Url Id</param>
        /// <param name="headers">request headers</param>
        /// <param name="userAgent">request user agent</param>
        /// <returns></returns>
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

        public async Task<int> ImportAsync(IEnumerable<ShortenedUrl> urls)
        {
            ShortenedUrls.AddRange(urls);
            return await SaveChangesAsync();
        }

        public async Task<List<ShortenedUrl>> ExportAsync()
        {
            return await ShortenedUrls.Take(1000).ToListAsync();
        }

        /// <summary>
        /// Shortened url details
        /// </summary>
        /// <param name="id">shortened url id</param>
        /// <returns>details of a specific shortened url</returns>
        public Task<ShortenedUrl> FindUrlByIdAsync(int id)
        {
            return Set<ShortenedUrl>().FindAsync(id);
        }

        #endregion

        /// <summary>
        /// Defaults protocol to HTTPS://
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        static string DefaultToHttpsProtocol(string url)
        {
            if (url.StartsWith("http"))
            {
                return url;
            }

            return "https://" + url;
        }

        /// <summary>
        /// Get title from an HTML string.
        /// </summary>
        static string GetTitle(string file)
        {
            var m = Regex.Match(file, @"<title>\s*(.+?)\s*</title>", RegexOptions.IgnoreCase);
            if (m.Success)
            {
                return m.Groups[1].Value;
            }

            return "";
        }
    }

    public class InvalidUrlExcpetion : Exception
    {
        public InvalidUrlExcpetion(string url, string message, Exception innerException)
            : base(message, innerException)
        {
            Url = url;
        }

        public string Url { get; set; }
    }
}