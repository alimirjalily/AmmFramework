﻿using AmmFramework.Extensions.Events.Abstractions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AmmFramework.Extensions.Events.Outbox.Dal.EF.Configs;

public class OutBoxEventItemConfig : IEntityTypeConfiguration<OutBoxEventItem>
{
    public void Configure(EntityTypeBuilder<OutBoxEventItem> builder)
    {
        builder.Property(c => c.AccruedByUserId).HasMaxLength(255);
        builder.Property(c => c.EventName).HasMaxLength(255);
        builder.Property(c => c.AggregateName).HasMaxLength(255);
        builder.Property(c => c.EventTypeName).HasMaxLength(500);
        builder.Property(c => c.AggregateTypeName).HasMaxLength(500);
        builder.Property(c => c.TraceId).HasMaxLength(100);
        builder.Property(c => c.SpanId).HasMaxLength(100);
        builder.ToTable("OutBoxEventItems", "AmmFramework");
    }
}