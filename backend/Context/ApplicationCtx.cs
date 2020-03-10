using Microsoft.EntityFrameworkCore;

using backend.Models;

namespace backend.Context
{
    public class ApplicationCtx : DbContext
    {
        public ApplicationCtx(DbContextOptions<ApplicationCtx> options) : base(options) { }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
    }
}