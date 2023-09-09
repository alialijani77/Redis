using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Caching.Distributed;
using Redis.Data.Interfaces;
using Redis.Domain.Entities;
using StackExchange.Redis;
using System.Text;
using System.Text.Json;

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
			//var content = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(book)).ToString();
			var content = JsonSerializer.Serialize(book).ToString();

			//var a = "Book_" + book.Name;
			//var b = "book".Trim().ToLower();

			//await _cache.SetAsync("Book_" +book.Name, content, new DistributedCacheEntryOptions { SlidingExpiration = TimeSpan.FromDays(1) });
			//_cache.Set("Book", content, new DistributedCacheEntryOptions { SlidingExpiration = TimeSpan.FromDays(1) });
			//_cache.SetStringAsync("Book", content, new DistributedCacheEntryOptions { SlidingExpiration = TimeSpan.FromDays(1) });
			//_cache.SetString("2", content);
			
			try
			{
				ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("127.0.0.1:6379");
				redis.GetDatabase().SetAdd("book", content);
				//redis.GetDatabase().SetAdd("1", Encoding.UTF8.GetBytes("alii"));
			}
			catch (Exception ex)
			{

				throw ;
			}


			return true;
		}

		public  async Task GetAllBooks()
		{
			var b = "book".Trim().ToLower();
			var r =  _cache.Get("b");
			var redisKeys = _muxer.GetServer("127.0.0.1", 6379).Keys().ToList();
			var redisKeyss = _muxer.GetServer("127.0.0.1", 6379).Keys().AsQueryable().Select(p => p.ToString()).ToList();
			var redisKeyss = _muxer.GetServer("127.0.0.1", 6379).Keys().AsQueryable().Select(p => p.ToString()).ToList();

			var result = new List<Book>();
			foreach (var item in redisKeys)
			{
				result.Add(JsonSerializer.Deserialize<Book>( _cache.GetString(item)));
			}
		//	return result;
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

		public async Task Delete(string key)
		{
			ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("127.0.0.1:6379");
			redis.GetDatabase().SetAdd("book", "ali");
			redis.GetDatabase().KeyDelete(key);

		}


	}
}
