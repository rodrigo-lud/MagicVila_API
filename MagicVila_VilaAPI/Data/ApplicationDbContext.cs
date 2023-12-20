using MagicVila_VilaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MagicVila_VilaAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> Options)
            : base(Options) { }
        public DbSet<Villa> Villas { get; set; }
    }
}
