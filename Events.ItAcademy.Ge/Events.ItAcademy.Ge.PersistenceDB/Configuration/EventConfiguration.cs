using Events.ItAcademy.Ge.Domain.POCO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Events.ItAcademy.Ge.PersistenceDB.Configuration
{
    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.ToTable(nameof(Event));
            builder.HasKey(x => x.EventID);
            builder.Property(x => x.Name).IsUnicode(false).IsRequired().HasMaxLength(80);
            builder.Property(x => x.City).IsRequired().IsUnicode(false).HasMaxLength(20);
            builder.Property(x => x.Address).IsRequired().IsUnicode(false).HasMaxLength(100);
            builder.Property(x => x.Date).IsRequired().HasColumnType("DateTime");
            builder.Property(x => x.PublisherID).IsRequired().HasColumnType("int");
            builder.HasOne(e => e.Publisher).WithMany(p => p.Events);
        }
    }
}
