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

public class BookShelfLayerConfig : IEntityTypeConfiguration<BookShelfLayer>
{
    public void Configure(EntityTypeBuilder<BookShelfLayer> builder)
    {
        builder.IsMultiTenant();

        builder
            .Property(b => b.LayerName)
                .HasMaxLength(1024);
    }
}

public class BookConfig : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.IsMultiTenant();

        builder
            .Property(b => b.Name)
                .HasMaxLength(1024);
        builder.Navigation(v => v.Items)
                  .UsePropertyAccessMode(PropertyAccessMode.Field);
        builder.HasMany(v => v.Items)
               .WithOne()
               .HasForeignKey(v => v.BookId)
               .OnDelete(DeleteBehavior.Cascade);

        // 建立索引
        builder.HasIndex(v => new { v.Name, v.IsBorrowed, v.Author, v.BookType });
    }
}

public class BookRecordConfig : IEntityTypeConfiguration<BookRecord>
{
    public void Configure(EntityTypeBuilder<BookRecord> builder)
    {
        builder.IsMultiTenant();
    }
}

