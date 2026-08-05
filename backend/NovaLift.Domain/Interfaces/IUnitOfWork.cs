using NovaLift.Domain.Entities;

namespace NovaLift.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IRepository<User> Users { get; }

    IRepository<Wallet> Wallets { get; }

    IRepository<Transaction> Transactions { get; }

    IRepository<Product> Products { get; }

    IRepository<TaskItem> Tasks { get; }

    // Add this
    IRepository<TaskTemplate> TaskTemplates { get; }

    IRepository<Order> Orders { get; }

    IRepository<Country> Countries { get; }

    IRepository<Language> Languages { get; }

    IRepository<VipConfig> VipConfigs { get; }

    IRepository<Referral> Referrals { get; }

    IRepository<Commission> Commissions { get; }

    IRepository<Banner> Banners { get; }

    IRepository<Announcement> Announcements { get; }

    IRepository<PaymentMethodConfig> PaymentMethodConfigs { get; }

    IRepository<Setting> Settings { get; }

    IRepository<AuditLog> AuditLogs { get; }


    Task<int> SaveChangesAsync();
}