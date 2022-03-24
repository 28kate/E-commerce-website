using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SalonApp.Models
{
    public class ArticleComment
    {
        
        public int Id { get; set;}
        public string Comments { get; set; }
        public DateTime PublishedDate { get; set; }
        public int ArticlesId { get; set; }
        public Article Articles { get; set; }
        public int Rating { get; set; }

    }
}