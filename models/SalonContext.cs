using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace SalonApp.Models
{
    public class SalonContext : DbContext
    {
        public SalonContext() : base("SalonDB")
        {

        }

        public virtual DbSet<TblCustomerReg> TblCustomerRegs { get; set; }
        public virtual DbSet<TblAdmin> TblAdmins { get; set; }
        public virtual DbSet<TblAppointments> TblAppointments { get; set; }
        public virtual DbSet<Tblproduct> Tblproducts { get; set; }

        public virtual DbSet<TblOrders> TblOrders { get; set; }
        //Latest
        public virtual DbSet<OrderMain> OrderMain { get; set; }
        public virtual DbSet<OrderItems> OrderItems { get; set; }
        public virtual DbSet<ShoppingCart> ShoppingCart { get; set; }
        //For the feedback
        public DbSet<BlogPost> BlogPost { get; set; }
        public DbSet<Comment> Comment { get; set; }
        //for the driver login
        public DbSet<TblDriverForLogin> tblDriverForLogins { get; set; }

        public virtual DbSet<Article> Articles { get; set; }
        public virtual DbSet<ArticleComment> ArticleComments { get; set; }


    }
}