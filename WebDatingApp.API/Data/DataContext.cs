using Microsoft.EntityFrameworkCore;
using WebDatingApp.API.Domain.Models;

namespace WebDatingApp.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base (options) {}
        // O nome do metodo é o nome da tabela criada no banco
        public DbSet<Value> Values { get; set; }
        public DbSet<User> Users { get; set; }
    }
}