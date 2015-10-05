using System;

namespace ShortUrl.Models
{
    public class ShortenedUrl
    {
        public int Id { get; set; }

        public string Url { get; set; }

        public DateTime Created { get; set; }
    }
}