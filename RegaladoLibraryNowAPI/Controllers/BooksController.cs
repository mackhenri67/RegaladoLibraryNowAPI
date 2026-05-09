using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RegaladoLibraryNowAPI.Models;
using static System.Net.WebRequestMethods;

namespace RegaladoLibraryNowAPI.Controllers
{
    [Route("api/v1/books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private static List<Book> books = new List<Book>
        {
            new Book
            {
                Id=1,
                Title = "The Notebook",
                Author = "Nicholas Sparks",
                Genre = "Romance",
                Available = true,
                PublishedYear = 1996
                },

            new Book
            {
                Id=2,
                Title = "Harry Potter and the Sorcerer's Stone book",
                Author = "J.K Rowling",
                Genre = "Fantasy",
                Available = true,
                PublishedYear = 1997
                }

        };

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(new
            {
                status = "success",
                data = books,
                message = "books retrieved"
            });
        }
        
        [HttpGet("[id]")]
        public IActionResult GetById(int id)
        {
            var book = books.FirstOrDefault(x => x.Id == id);
            if (book == null)
            {
                return NotFound(new
                {
                    status = "error",
                    data = (object?)null,
                    message = "book not found"
                });

            }

            return Ok(new
            {
                status = "success",
                data = books,
                message = "books retrieved"
            });
        }

        [HttpPost]
        
        public IActionResult Create([FromBody] Book newBook)
        {
            newBook.Id = books.Count + 1;
            books.Add(newBook);
            return CreatedAtAction(nameof(GetById),
                new { id = newBook.Id },
                new
                {
                    status = "success",
                    data = newBook,
                    message = "Book created."
                });

        }


        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Book updateBook)
        {
            var book = books.FirstOrDefault(x =>x.Id == id);
            if (book == null)
                return NotFound(new
                {
                    status = "error",
                    data = (object?)null,
                    message = "Book not found."
                });

            book.Title = updateBook.Title;
            book.Author = updateBook.Author;
            book.Genre = updateBook.Genre;
            book.Available = updateBook.Available;
            book.PublishedYear = updateBook.PublishedYear;

            return Ok(new
            {
                status = "success",
                data = book,
                message = "Book updated."
            });
            
    
    }

        [HttpDelete("{id}")]

        public IActionResult Delete(int id)
        {
            var book = books.FirstOrDefault(x =>x.Id == id);
            if (book == null)
                return NotFound(new
                {
                    status = "error",
                    data = (object?)null,
                    message = "Book not found."
                });

            books.Remove(book);
            return Ok(new
            {
                status = "success",
                data = book,
                message = "Book deleted."
            });
        }
        
        }
}
