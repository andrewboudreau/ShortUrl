using System;

namespace ShortUrl.Models
{
    public class Visitor
    {
        public int ShortUrlId { get; set; }

        public string Headers { get; set; }

        public string Agent { get; set; }

        public DateTime Created { get; set; }
    }
}