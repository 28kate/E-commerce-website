using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace SalonApp.Models
{
    public class BlogPost
    {
        public int BlogPostID { get; set; }

        [Display(Name ="Name")]
        public string Title { get; set; }

        [Column(TypeName = "ntext")]
        [Display(Name ="Comment")]
        public string Body { get; set; }
    }
}