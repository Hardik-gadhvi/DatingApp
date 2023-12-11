using Dating_App_Main.Models;
using Microsoft.EntityFrameworkCore;

namespace Dating_App_Main.Data
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options) { }

        public DbSet<UserModel> Users { get; set; }
    }
}
 