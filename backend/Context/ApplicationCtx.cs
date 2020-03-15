using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

using backend.Models;

namespace backend.Context
{
    public class ApplicationCtx : DbContext
    {
        public ApplicationCtx(DbContextOptions<ApplicationCtx> opts) : base(opts) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Author>().HasMany(a => a.Books);
            builder.Entity<Book>().HasOne(b => b.Author);
        }

        [Description("Collection of books")]
        public DbSet<Book> Books { get; set; }
        [Description("Collection of authors")]
        public DbSet<Author> Author { get; set; }
    }
}