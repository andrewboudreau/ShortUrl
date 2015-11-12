using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;
using ShortUrl.Models;

namespace ShortUrl.Service
{
    public interface IShortUrlService
    {
        Task<ShortenedUrl> FindUrlByIdAsync(int id);

        Task DeleteUrlByIdAsync(int id);

        Task<int> ShortenUrlAsync(string url);

        Task<List<ShortenedUrl>> RecentShortenedUrls();
        
        Task AddVisitorAsync(int id, NameValueCollection headers, string userAgent);

        Task<int> ImportAsync(IEnumerable<ShortenedUrl> urls);

        Task<List<ShortenedUrl>> ExportAsync();
    }
}