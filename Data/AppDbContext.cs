using GlobalBankApi.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;


namespace GlobalBankApi.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }
        public DbSet<ContaBancaria> ContaBancarias {get; set;}
        public DbSet<Transacao> Transacoes {get; set;}
    }
}