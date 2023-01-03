using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DesafioApi.Models
{
    public class DesafioContext : DbContext
    {
        public DesafioContext(DbContextOptions<DesafioContext> options)
        : base(options)
        {}

        public DbSet<Servidor> Servidor { get; set; } = null!;
        public DbSet<Video> Video { get; set; } = null!;
    }
}