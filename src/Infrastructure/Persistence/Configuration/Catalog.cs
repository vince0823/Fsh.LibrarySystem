using Finbuckle.MultiTenant.EntityFrameworkCore;
using FSH.Learn.Domain.Catalog;
using FSH.Learn.Domain.System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FSH.Learn.Infrastructure.Persistence.Configuration;

public class BrandConfig : IEntityTypeConfiguration<Brand>
{
    public void Configure(EntityTypeBuilder<Brand> builder)
    {
        builder.IsMultiTenant();

        builder
            .Property(b => b.Name)
                .HasMaxLength(256);
    }
}

public class ProductConfig : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.IsMultiTenant();

        builder
            .Property(b => b.Name)
                .HasMaxLength(1024);

        builder
            .Property(p => p.ImagePath)
                .HasMaxLength(2048);
    }
}

public class DepartmentConfig : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        builder.IsMultiTenant();

        builder
            .Property(b => b.Name)
                .HasMaxLength(1024);

    }
}

public class MenuConfig : IEntityTypeConfiguration<Menu>
{
    public void Configure(EntityTypeBuilder<Menu> builder)
    {
        builder.IsMultiTenant();

        builder
            .Property(b => b.Name)
                .HasMaxLength(1024);

    }
}

public class BookRoomConfig : IEntityTypeConfiguration<BookRoom>
{
    public void Configure(EntityTypeBuilder<BookRoom> builder)
    {
        builder.IsMultiTenant();

        builder
            .Property(b => b.Name)
                .HasMaxLength(1024);

    }
}

public class BookShelfConfig : IEntityTypeConfiguration<BookShelf>
{
    public void Configure(EntityTypeBuilder<BookShelf> builder)
    {
        builder.IsMultiTenant();

        builder
            .Property(b => b.Code)
                .HasMaxLength(1024);
    }
}
