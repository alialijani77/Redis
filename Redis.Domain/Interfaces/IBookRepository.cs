using Redis.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redis.Domain.Interfaces
{
	public interface IBookRepository
	{
		Task<Book> GetById(string book);

		bool CreateBook(Book book);

		List<Book> GetAllBooks();
	}
}
