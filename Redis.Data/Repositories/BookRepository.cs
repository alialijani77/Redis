using Microsoft.Extensions.Caching.Distributed;
using Redis.Domain.Entities;
using Redis.Domain.Interfaces;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Redis.Data.Repositories
{
	public class BookRepository : IBookRepository
	{
		private readonly IDistributedCache _cache;
		private readonly IConnectionMultiplexer _muxer;
		public BookRepository(IDistributedCache cache, IConnectionMultiplexer muxser)
		{
			_cache = cache;
			_muxer = muxser;
		}

		public  bool CreateBook(Book book)
		{
			var content = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(book));
			var a = "Book_" + book.Name;
			var b = "book".Trim().ToLower();

			//await _cache.SetAsync("Book_" +book.Name, content, new DistributedCacheEntryOptions { SlidingExpiration = TimeSpan.FromDays(1) });
			_cache.Set(b, content, new DistributedCacheEntryOptions { SlidingExpiration = TimeSpan.FromDays(1) });

			return true;
		}

		public  List<Book> GetAllBooks()
		{
			var b = "book".Trim().ToLower();
			var redisKeys = _muxer.GetServer("127.0.0.1", 6379).Keys().AsQueryable().Select(p => p.ToString()).ToList();
			var result = new List<Book>();

			foreach (var item in redisKeys)
			{
				result.Add(JsonSerializer.Deserialize<Book>( _cache.GetString(item)));
			}
			return result;
		}

		public async Task<Book> GetById(string book)
		{
			var bookContent = await _cache.GetStringAsync(book);

			if (bookContent == null)
			{
				return null;
			}
			return JsonSerializer.Deserialize<Book>(bookContent);
		}


	}
}
