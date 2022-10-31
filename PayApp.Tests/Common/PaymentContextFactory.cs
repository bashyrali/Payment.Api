using System;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace PayApp.Tests.Common
{
    public class PaymentContextFactory
    {
        public static PaymentDbContext Create()
        {
            var options = new DbContextOptionsBuilder<PaymentDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new PaymentDbContext(options);
            context.Database.EnsureCreated();
            context.SaveChanges();
            return context;
        }

        public static void Destroy(PaymentDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}