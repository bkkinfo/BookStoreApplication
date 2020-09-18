using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BookStoreApplication.Models;
using BookStoreApplication.Models.Data;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookStoreApplication.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        // GET: api/Books
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        /**
        // GET: api/Books/5
        [HttpGet("{pageNum}", Name = "Get")]
           public IEnumerable<Book> Get(int pageNum)
         {
             int pageSize = 4;
             int startPosition = (pageNum - 1) * pageSize;

           using (var context = new LibraryDbContext())
             {
                 List<Book> books = context.Books.ToList();
                 if (books.Count >= startPosition)
                 {
                     return books.Skip(startPosition).Take(pageSize);
                 }
                 else
                 {
                     return new List<Book>();
                 }
             } 
   }
        **/

    }

}
