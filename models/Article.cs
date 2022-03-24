using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalonApp.Models
{
    public class Article
    {

        public int Id { get; set; }
        public string UserName { get; set; }

        public string Description { get; set; }
        public int Rating { get; set; }
      

        public ICollection<ArticleComment> ArticleComments { get; set; }
    }
}