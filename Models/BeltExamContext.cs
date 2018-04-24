using Microsoft.EntityFrameworkCore;
 
namespace C_Sharp_Belt_II.Models
{
    public class BeltExamContext : DbContext
    {
        public BeltExamContext(DbContextOptions<BeltExamContext> options) : base(options) { }

        public DbSet<User> users { get; set; }

        public DbSet<Ideas> ideas { get; set; }
        public DbSet<Likes> likes { get; set; }
    }
}