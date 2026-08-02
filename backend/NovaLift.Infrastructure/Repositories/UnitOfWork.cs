using NovaLift.Domain.Entities;
using NovaLift.Domain.Interfaces;
using NovaLift.Infrastructure.Data;

namespace NovaLift.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public IRepository<User> Users { get; }
    public IRepository<Wallet> Wallets { get; }
    public IRepository<Transaction> Transactions { get; }
    public IRepository<Product> Products { get; }
    public IRepository<TaskItem> Tasks { get; }
    public IRepository<Order> Orders { get; }
    public IRepository<Country> Countries { get; }
    public IRepository<Language> Languages { get; }
    public IRepository<VipConfig> VipConfigs { get; }
    public IRepository<Referral> Referrals { get; }
    public IRepository<Commission> Commissions { get; }
    public IRepository<Banner> Banners { get; }
    public IRepository<Announcement> Announcements { get; }
    public IRepository<PaymentMethodConfig> PaymentMethodConfigs { get; }
    public IRepository<Setting> Settings { get; }
    public IRepository<AuditLog> AuditLogs { get; }

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
        Users = new Repository<User>(context);
        Wallets = new Repository<Wallet>(context);
        Transactions = new Repository<Transaction>(context);
        Products = new Repository<Product>(context);
        Tasks = new Repository<TaskItem>(context);
        Orders = new Repository<Order>(context);
        Countries = new Repository<Country>(context);
        Languages = new Repository<Language>(context);
        VipConfigs = new Repository<VipConfig>(context);
        Referrals = new Repository<Referral>(context);
        Commissions = new Repository<Commission>(context);
        Banners = new Repository<Banner>(context);
        Announcements = new Repository<Announcement>(context);
        PaymentMethodConfigs = new Repository<PaymentMethodConfig>(context);
        Settings = new Repository<Setting>(context);
        AuditLogs = new Repository<AuditLog>(context);
    }

    public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();
    public void Dispose() => _context.Dispose();
}
