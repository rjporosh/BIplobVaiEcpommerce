using CRUDOperation.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace CRUDOperation.DatabaseContext.FluentConfiguration
{
    class ProductFluentConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(c => c.Name).IsRequired();
        }
    }
}
