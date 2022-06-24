using BookStoreAPI.Model;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStoreAPI.Repository
{
    public interface IBookRepository
    {
        Task<List<BookModel>> GetAllBooksAsync();
        Task<BookModel> GetBookByIdAsync(int bookId);
        Task<int> AddBookAsync(BookModel bookModel);

        Task UpdateBookByIdAsync(int bookId, BookModel bookModel);

        Task UpdateBookPatchAsync(int bookId, JsonPatchDocument bookModel);

         Task DeleteBookAsync(int bookId);
    }
}
