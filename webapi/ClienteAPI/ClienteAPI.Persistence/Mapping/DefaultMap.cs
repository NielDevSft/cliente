﻿using ClienteAPI.Domain.Core.Models;
using ClienteAPI.Persistence.Extentions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClienteAPI.Persistence.Mapping
{
    public class DefaultMap<T> : EntityTypeConfiguration<T> where T : Entity<T>
    {
        public override void Map(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(e => e.Uuid);

            builder.Property(e => e.Active)
                .IsRequired();

            builder.Property(e => e.Removed)
                .IsRequired();

            builder.Ignore(e => e.ClassLevelCascadeMode);
            builder.Ignore(e => e.RuleLevelCascadeMode);
        }
    }
}
