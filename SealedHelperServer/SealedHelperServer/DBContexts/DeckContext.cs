using Microsoft.EntityFrameworkCore;
using SealedHelperServer.Models;

namespace SealedHelperServer.DBContexts
{
    public class DeckContext : DbContext
    {
        public DbSet<Deck> Decks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlite("Datasource=DecksDatabase");
        }
    }
}