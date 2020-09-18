using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreApplication.Models
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }
        [Required]
        public string BookName { get; set; }
        [Required]
        public string BookAuthor { get; set; }
        public string BookPublisher { get; set; }
        [Required]
        public Double BookPrice { get; set; }      
        public User BookEnrollUser { get; set; }
        public User BookSubscription { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int Stock { get; set; }
        [Required]
        public string Image { get; set; }
    }
}
