using Microsoft.EntityFrameworkCore;
using NovaLift.Domain.Entities;
using NovaLift.Domain.Enums;

namespace NovaLift.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Wallet> Wallets => Set<Wallet>();
    public DbSet<Transaction> Transactions => Set<Transaction>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<TaskItem> Tasks => Set<TaskItem>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<Country> Countries => Set<Country>();
    public DbSet<Language> Languages => Set<Language>();
    public DbSet<VipConfig> VipConfigs => Set<VipConfig>();
    public DbSet<Referral> Referrals => Set<Referral>();
    public DbSet<Commission> Commissions => Set<Commission>();
    public DbSet<Banner> Banners => Set<Banner>();
    public DbSet<Announcement> Announcements => Set<Announcement>();
    public DbSet<PaymentMethodConfig> PaymentMethodConfigs => Set<PaymentMethodConfig>();
    public DbSet<Setting> Settings => Set<Setting>();
    public DbSet<AuditLog> AuditLogs => Set<AuditLog>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // User
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasIndex(u => u.Email).IsUnique();
            entity.HasIndex(u => u.Phone).IsUnique();
            entity.HasIndex(u => u.ReferralCode).IsUnique();
            entity.HasIndex(u => u.ReferredBy);
            entity.Property(u => u.Balance).HasPrecision(18, 2);
            entity.Property(u => u.FrozenBalance).HasPrecision(18, 2);
            entity.Property(u => u.TotalDeposited).HasPrecision(18, 2);
            entity.Property(u => u.TotalWithdrawn).HasPrecision(18, 2);
            entity.Property(u => u.TotalEarned).HasPrecision(18, 2);
            entity.HasOne(u => u.Referrer).WithMany().HasForeignKey(u => u.ReferredBy);
            entity.HasOne(u => u.Country).WithMany(c => c.Users).HasForeignKey(u => u.CountryId);
        });

        // Wallet
        modelBuilder.Entity<Wallet>(entity =>
        {
            entity.HasIndex(w => w.UserId);
            entity.HasOne(w => w.User).WithMany(u => u.Wallets).HasForeignKey(w => w.UserId);
        });

        // Transaction
        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasIndex(t => t.UserId);
            entity.HasIndex(t => t.Status);
            entity.HasIndex(t => t.Type);
            entity.HasIndex(t => t.TransactionNumber);
            entity.Property(t => t.Amount).HasPrecision(18, 2);
            entity.Property(t => t.Fee).HasPrecision(18, 2);
            entity.Property(t => t.NetAmount).HasPrecision(18, 2);
            entity.HasOne(t => t.User).WithMany(u => u.Transactions).HasForeignKey(t => t.UserId);
            entity.HasOne(t => t.Wallet).WithMany().HasForeignKey(t => t.WalletId);
            entity.HasOne(t => t.Reviewer).WithMany().HasForeignKey(t => t.ReviewedBy);
        });

        // Product
        modelBuilder.Entity<Product>(entity =>
        {
            entity.Property(p => p.Price).HasPrecision(18, 2);
            entity.Property(p => p.CommissionRate).HasPrecision(5, 2);
        });

        // Task
        modelBuilder.Entity<TaskItem>(entity =>
        {
            entity.HasIndex(t => t.UserId);
            entity.HasIndex(t => t.Status);
            entity.Property(t => t.Reward).HasPrecision(18, 2);
            entity.HasOne(t => t.User).WithMany(u => u.Tasks).HasForeignKey(t => t.UserId);
            entity.HasOne(t => t.Product).WithMany(p => p.Tasks).HasForeignKey(t => t.ProductId);
        });

        // Order
        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasIndex(o => o.UserId);
            entity.HasIndex(o => o.Status);
            entity.Property(o => o.UnitPrice).HasPrecision(18, 2);
            entity.Property(o => o.TotalPrice).HasPrecision(18, 2);
            entity.Property(o => o.Commission).HasPrecision(18, 2);
            entity.HasOne(o => o.User).WithMany(u => u.Orders).HasForeignKey(o => o.UserId);
            entity.HasOne(o => o.Product).WithMany(p => p.Orders).HasForeignKey(o => o.ProductId);
        });

        // Country
        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasIndex(c => c.Code).IsUnique();
        });

        // Language
        modelBuilder.Entity<Language>(entity =>
        {
            entity.HasIndex(l => l.Code).IsUnique();
        });

        // VIP Config
        modelBuilder.Entity<VipConfig>(entity =>
        {
            entity.HasIndex(v => v.Level).IsUnique();
            entity.Property(v => v.MinDeposit).HasPrecision(18, 2);
            entity.Property(v => v.CommissionRate).HasPrecision(5, 2);
            entity.Property(v => v.TaskRewardMultiplier).HasPrecision(5, 2);
            entity.Property(v => v.WithdrawalFee).HasPrecision(5, 2);
            entity.Property(v => v.DailyWithdrawalLimit).HasPrecision(18, 2);
        });

        // Referral
        modelBuilder.Entity<Referral>(entity =>
        {
            entity.HasIndex(r => r.ReferrerId);
            entity.Property(r => r.Commission).HasPrecision(18, 2);
            entity.HasOne(r => r.Referrer).WithMany().HasForeignKey(r => r.ReferrerId);
            entity.HasOne(r => r.Referred).WithMany().HasForeignKey(r => r.ReferredId);
        });

        // Commission
        modelBuilder.Entity<Commission>(entity =>
        {
            entity.HasIndex(c => c.UserId);
            entity.Property(c => c.Amount).HasPrecision(18, 2);
            entity.HasOne(c => c.User).WithMany(u => u.Commissions).HasForeignKey(c => c.UserId);
        });

        // PaymentMethodConfig
        modelBuilder.Entity<PaymentMethodConfig>(entity =>
        {
            entity.HasIndex(p => p.Type).IsUnique();
            entity.Property(p => p.MinDeposit).HasPrecision(18, 2);
            entity.Property(p => p.MaxDeposit).HasPrecision(18, 2);
            entity.Property(p => p.MinWithdrawal).HasPrecision(18, 2);
            entity.Property(p => p.MaxWithdrawal).HasPrecision(18, 2);
            entity.Property(p => p.DepositFee).HasPrecision(5, 2);
            entity.Property(p => p.WithdrawalFee).HasPrecision(5, 2);
        });

        // Settings
        modelBuilder.Entity<Setting>(entity =>
        {
            entity.HasIndex(s => s.Key).IsUnique();
        });

        // AuditLog
        modelBuilder.Entity<AuditLog>(entity =>
        {
            entity.HasIndex(a => a.UserId);
            entity.HasIndex(a => a.Action);
            entity.HasOne(a => a.User).WithMany().HasForeignKey(a => a.UserId);
        });
    }
}
