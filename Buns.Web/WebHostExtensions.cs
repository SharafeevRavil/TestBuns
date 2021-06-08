using System;
using System.Linq;
using Buns.Data;
using Buns.Domain.Entities.Buns;
using Buns.Domain.Factories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Buns.Web
{
    public static class WebHostExtensions
    {
        public static IHost SeedData(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;

            var context = services.GetService<ApplicationDbContext>();
            var bunFactory = services.GetService<BunFactory>();
            if (context == null || bunFactory == null)
                return host;

            var bunTypes = new[]
            {
                new BunType("Круассан"),
                new BunType(PretzelBun.BunTypeName),
                new BunType("Багет"),
                new BunType(SourCreamBun.BunTypeName),
                new BunType("Батон")
            };

            var buns = new[]
            {
                bunFactory.CreateBun(bunTypes[0], 100, 2, 1),
                bunFactory.CreateBun(bunTypes[2], 100, 4, 0, DateTimeOffset.Now.AddHours(-1).AddMinutes(2)),
                bunFactory.CreateBun(bunTypes[4], 100, 4, 0, DateTimeOffset.Now.AddHours(-2).AddMinutes(5)),
                bunFactory.CreateBun(bunTypes[4], 100, 4, 0, DateTimeOffset.Now.AddHours(-3).AddMinutes(8)),

                bunFactory.CreateBun(bunTypes[1], 100, 6, 1),
                bunFactory.CreateBun(bunTypes[1], 100, 6, 0, DateTimeOffset.Now.AddHours(-1).AddMinutes(12)),
                bunFactory.CreateBun(bunTypes[1], 100, 6, 0, DateTimeOffset.Now.AddHours(-2).AddMinutes(15)),
                bunFactory.CreateBun(bunTypes[1], 100, 6, 0, DateTimeOffset.Now.AddHours(-3).AddMinutes(18)),

                bunFactory.CreateBun(bunTypes[3], 100, 6, 1),
                bunFactory.CreateBun(bunTypes[3], 100, 6, 0, DateTimeOffset.Now.AddHours(-1).AddMinutes(22)),
                bunFactory.CreateBun(bunTypes[3], 100, 6, 0, DateTimeOffset.Now.AddHours(-2).AddMinutes(25)),
                bunFactory.CreateBun(bunTypes[3], 100, 6, 0, DateTimeOffset.Now.AddHours(-3).AddMinutes(28)),
            };

            if (!context.Buns.Any())
                context.Buns.AddRange(buns);

            context.SaveChanges();

            return host;
        }
    }
}