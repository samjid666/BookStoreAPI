using BookStoreAPI.Data;
using System;

namespace BookStoreAPI.Model
{
    public class BookModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        internal void ApplyTo(Books book)
        {
            throw new NotImplementedException();
        }
    }
}
