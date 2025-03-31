# ShortUrl - A Minimal URL Shortener

ShortUrl is an ultra-lightweight URL shortening service built with C# Minimal API. It's designed to be as simple as possible while providing all the core functionality of a URL shortener.

## Features

- Create shortened URLs with a simple GET request
- Redirect to original URLs using short IDs
- Thread-safe for concurrent requests
- Extremely minimal codebase (less than 40 lines of code)
- Built with ASP.NET Core Minimal API

## How It Works

ShortUrl uses incrementing integers for short URLs. Each original URL is assigned a unique number, making the shortened URLs as compact as possible (e.g., `http://yoursite.com/42`).

## API Endpoints

### Shorten a URL
`GET /shorten?url=https://example.com`

Response:
```json
{
  "shortUrl": "http://localhost:5000/1",
  "originalUrl": "https://example.com",
  "id": 1
}
```

### Access a shortened URL
`GET /{id}`

## Example
```bash
# Create a short URL
curl "http://localhost:5000/shorten?url=https://example.com"

# Use the short URL (will redirect to the original URL)
curl -L "http://localhost:5000/1"
```

## How to Extend
This is deliberately minimal. Some ways you might want to extend it:

Add persistent storage (JSON, database, etc.)
- Implement custom URL slugs
- Add authentication for URL creation
- Add URL analytics (click tracking)
- Implement URL expiration