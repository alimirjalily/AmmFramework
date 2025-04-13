using AmmFramework.Extensions.Events.Abstractions;
using AmmFramework.Extensions.Events.Outbox.Dal.EF.Configs;
using AmmFramework.Extensions.Events.Outbox.Dal.EF.Interceptors;
using AmmFramework.Infra.Data.Sql.Commands;

namespace AmmFramework.Extensions.Events.Outbox.Dal.EF;

public abstract class BaseOutboxCommandDbContext : BaseCommandDbContext
{
    public DbSet<OutBoxEventItem> OutBoxEventItems { get; set; }

    public BaseOutboxCommandDbContext(DbContextOptions options) : base(options)
    {

    }

    protected BaseOutboxCommandDbContext()
    {
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.AddInterceptors(new AddOutBoxEventItemInterceptor());
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfiguration(new OutBoxEventItemConfig());
    }


}