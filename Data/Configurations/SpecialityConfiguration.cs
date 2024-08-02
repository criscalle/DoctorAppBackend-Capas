using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Configurations;

public class SpecialityConfiguration : IEntityTypeConfiguration<Speciality>
{
    public void Configure(EntityTypeBuilder<Speciality> builder)
    {
        builder.Property(x => x.Id).IsRequired();
        builder.Property(x => x.namespeciality).IsRequired().HasMaxLength(60);
        builder.Property(x => x.description).IsRequired().HasMaxLength(100);
        builder.Property(x => x.state).IsRequired();
    }
}
