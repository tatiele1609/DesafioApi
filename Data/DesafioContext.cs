using DesafioApi.Models;
using Microsoft.EntityFrameworkCore;

namespace DesafioApi.Data
{
    public class DesafioContext : DbContext
    {
        public DesafioContext(DbContextOptions<DesafioContext> options)
        : base(options)
        {}

        public DbSet<Servidor> Servidor { get; set; } = null!;
        public DbSet<Video> Video { get; set; } = null!;
        public DbSet<Arquivo> Arquivo { get; set; } = null!;
    }
}