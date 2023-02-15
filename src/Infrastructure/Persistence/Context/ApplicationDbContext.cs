using Finbuckle.MultiTenant;
using FSH.Learn.Application.Common.Events;
using FSH.Learn.Application.Common.Interfaces;
using FSH.Learn.Domain.Catalog;
using FSH.Learn.Infrastructure.Persistence.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using FSH.Learn.Domain.System;

namespace FSH.Learn.Infrastructure.Persistence.Context;

public class ApplicationDbContext : BaseDbContext
{
    public ApplicationDbContext(ITenantInfo currentTenant, DbContextOptions options, ICurrentUser currentUser, ISerializerService serializer, IOptions<DatabaseSettings> dbSettings, IEventPublisher events)
        : base(currentTenant, options, currentUser, serializer, dbSettings, events)
    {
    }

    public DbSet<Product> Products => Set<Product>();
    public DbSet<Brand> Brands => Set<Brand>();
    public DbSet<Department> Departments => Set<Department>();
    public DbSet<Menu> Menus => Set<Menu>();
    public DbSet<RoleMenu> RoleMenus => Set<RoleMenu>();

    public DbSet<BookRoom> BookRooms => Set<BookRoom>();
    public DbSet<BookShelf> BookShelfs => Set<BookShelf>();
    public DbSet<BookShelfLayer> BookShelfLayers => Set<BookShelfLayer>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema(SchemaNames.Catalog);
    }
}