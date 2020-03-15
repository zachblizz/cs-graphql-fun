using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using backend.Models;
using backend.Context;

namespace backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IHost host = CreateHostBuilder(args).Build();
            using (IServiceScope scope = host.Services.CreateScope())
            {
                Seed(scope);
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        private static void Seed(IServiceScope scope)
        {
            // SEED the DB
            ApplicationCtx ctx = scope.ServiceProvider.GetRequiredService<ApplicationCtx>();

            var authorDbEntry = ctx.Author.Add(
                new Author
                {
                    Name = "jimmyfargo",
                }
            );

            ctx.SaveChanges();

            ctx.Books.AddRange(
                new Book
                {
                    Id = "1",
                    Name = "First Book",
                    Published = true,
                    AuthorId = authorDbEntry.Entity.Id,
                    Genre = "Mystery"
                },
                new Book
                {
                    Id = "2",
                    Name = "Second Book",
                    Published = true,
                    AuthorId = authorDbEntry.Entity.Id,
                    Genre = "Crime"
                }
            );

            ctx.SaveChanges();
        }
    }
}
