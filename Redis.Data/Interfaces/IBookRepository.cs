using Redis.Domain.Entities;

namespace Redis.Data.Interfaces
{
	public interface IBookRepository
	{
		Task<Book> GetById(string book);

		bool CreateBook(Book book);

		Task GetAllBooks();
	}
}
