using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Redis.Domain.Entities;
using Redis.Domain.Interfaces;
using System.Text.Json;
using System.Text;
using StackExchange.Redis;

namespace Redis.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BookController : ControllerBase
	{
		private readonly IBookRepository _bookRepository;
		public BookController(IBookRepository bookRepository)
		{
			_bookRepository = bookRepository;
		}

		[HttpPost]
		public async Task<IActionResult> CreateBook(Book book)
		{
			 _bookRepository.CreateBook(book);
			return Ok();

		}

		[HttpGet]
		public  IActionResult GetAllBooks()
		{
			var result =  _bookRepository.GetAllBooks();
			return Ok(result);
		}
	}
}
