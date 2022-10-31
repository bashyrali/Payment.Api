using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces
{
    public interface IPaymentDbContext
    {
        DbSet<PaymentInfo> Payments { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}