using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;

using ShortUrl.Models;

namespace ShortUrl.Services
{
    public interface IShortUrlService
    {
        Task<ShortenedUrl> FindUrlByIdAsync(int id);

        Task<int> ShortenUrlAsync(string url);

        IQueryable<ShortenedUrl> RecentShortenedUrls();

        Task AddVisitorAsync(int id, NameValueCollection headers, string userAgent);
    }
}