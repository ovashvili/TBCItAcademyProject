using Events.ItAcademy.Ge.Domain.POCO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Events.ItAcademy.Ge.PersistenceDB.Configuration
{
    public class PublisherConfiguration : IEntityTypeConfiguration<Publisher>
    {
        public void Configure(EntityTypeBuilder<Publisher> builder)
        {
            builder.ToTable(nameof(Publisher));
            builder.HasKey(x => x.PublisherID);
            builder.Property(x => x.Name).IsUnicode(false).IsRequired().HasMaxLength(80);
            builder.HasMany(e => e.Events).WithOne(p => p.Publisher);
        }
    }
}
