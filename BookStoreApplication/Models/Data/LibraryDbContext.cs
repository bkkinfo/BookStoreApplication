using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreApplication.Models.Data
{
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext()
        {
        }

        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options)
            {

            }
            public DbSet<Book> Books { get; set; }
            public DbSet<User> Users { get; set; }
        
    }
}
