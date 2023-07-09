using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configuration
{
    public class ClientConfig : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable("Clients");
            builder.HasKey(x => x.Id);
            builder.Property(p=> p.FirstName).HasMaxLength(80).IsRequired();
            builder.Property(p => p.LastName).HasMaxLength(80).IsRequired();
            builder.Property(p => p.Birthday).IsRequired();
            builder.Property(p => p.Telephone).HasMaxLength(9).IsRequired();
            builder.Property(p => p.Email).HasMaxLength(100);
            builder.Property(p => p.Address).HasMaxLength(120).IsRequired();
            builder.Property(p => p.Age);
            builder.Property(p => p.CreateBy).HasMaxLength(30);
            builder.Property(p => p.LastModifiedBy).HasMaxLength(30);
        }
    }
}
