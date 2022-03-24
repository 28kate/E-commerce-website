using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SalonApp.Models;
using System.ComponentModel.DataAnnotations;

namespace SalonApp.Views
{
    public class ArticleCommentViewModel
    {
        public List<ArticleComment> ListOfComments { get; set; }
        public string Comments { get; set;}
        [Key]
        public int ArticleId { get; set; }
        public int Rating { get; set; }

    }
}