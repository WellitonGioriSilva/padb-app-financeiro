using ApiFinanceiro.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiFinanceiro.DataContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Despesa> Despesas { get; set; }
        public DbSet<Receita> Receitas { get; set; }
    }
}
