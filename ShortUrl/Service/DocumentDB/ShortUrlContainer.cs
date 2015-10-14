using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;
using ShortUrl.Models;

namespace ShortUrl.Service.DocumentDB
{
    public class ShortUrlContainer : IShortUrlService
    {
        public Task<ShortenedUrl> FindUrlByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteUrlByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> ShortenUrlAsync(string url)
        {
            throw new NotImplementedException();
        }

        public Task<List<ShortenedUrl>> RecentShortenedUrls()
        {
            throw new NotImplementedException();
        }

        public Task AddVisitorAsync(int id, NameValueCollection headers, string userAgent)
        {
            throw new NotImplementedException();
        }
    }
}