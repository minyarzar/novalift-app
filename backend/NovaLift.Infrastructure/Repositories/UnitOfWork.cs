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
    public IRepository<TaskTemplate> TaskTemplates { get; }


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


        Users = new Repository<User>(_context);
        Wallets = new Repository<Wallet>(_context);
        Transactions = new Repository<Transaction>(_context);
        Products = new Repository<Product>(_context);
        Tasks = new Repository<TaskItem>(_context);
        TaskTemplates = new Repository<TaskTemplate>(_context);


        Orders = new Repository<Order>(_context);
        Countries = new Repository<Country>(_context);
        Languages = new Repository<Language>(_context);
        VipConfigs = new Repository<VipConfig>(_context);
        Referrals = new Repository<Referral>(_context);
        Commissions = new Repository<Commission>(_context);
        Banners = new Repository<Banner>(_context);
        Announcements = new Repository<Announcement>(_context);
        PaymentMethodConfigs = new Repository<PaymentMethodConfig>(_context);
        Settings = new Repository<Setting>(_context);
        AuditLogs = new Repository<AuditLog>(_context);
    }


    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }


    public void Dispose()
    {
        _context.Dispose();
    }
}