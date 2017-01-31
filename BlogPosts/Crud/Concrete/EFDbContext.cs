using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Crud.Models;


namespace Crud.Concrete
{
    public class EFDbContext : DbContext
    {    
        public DbSet<PostModel> Posts { get; set; }
        public DbSet<ImageModel> Images { get; set; }
 
    }
}