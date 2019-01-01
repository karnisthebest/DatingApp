using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Data
{
    public class DataContext : DbContext
    {
        //in order for dbcontext to work we need to add options
        public DataContext(DbContextOptions<DataContext> options) : base(options){}
   
        //create a table called Values when migrate entity framework 
        public DbSet<Value> Values { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<Photo> Photos { get; set; }
    }
}