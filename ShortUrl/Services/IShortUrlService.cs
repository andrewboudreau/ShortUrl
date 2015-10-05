using System.Collections.Generic;

using ShortUrl.Models;

namespace ShortUrl.Services
{
    public interface IShortUrlService
    {
        ShortenedUrl Find(int id);

        int Create(string url);

        IList<ShortenedUrl> Top100();
    }
}