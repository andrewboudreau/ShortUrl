using System;

namespace ShortUrl.Models
{
    public class ShortenedUrl
    {
        public int Id { get; set; }

        public string Url { get; set; }

        public int HttpStatusCode { get; set; }

        public string Title { get; set; }

        public DateTime Created { get; set; }
    }
}