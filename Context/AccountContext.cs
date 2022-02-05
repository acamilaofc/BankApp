using BankApp.Model;
using Microsoft.EntityFrameworkCore;

namespace BankApp.Context
{
    public class AccountContext : DbContext
    {
        public DbSet<Account>? Accounts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=BankApp.db");
        }
    }
}