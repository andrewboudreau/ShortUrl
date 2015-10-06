using System.Collections.Generic;
using System.Threading.Tasks;

using ShortUrl.Models;

namespace ShortUrl.Services
{
    public interface IShortUrlService
    {
        Task<ShortenedUrl> GetAsync(int id);

        Task<int> CreateAsync(string url);

        Task<List<ShortenedUrl>> RecentShortenedUrls();
    }
}