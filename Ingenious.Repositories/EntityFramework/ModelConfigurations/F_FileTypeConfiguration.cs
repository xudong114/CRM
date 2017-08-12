﻿using Ingenious.Domain.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Ingenious.Repositories.EntityFramework.ModelConfigurations
{
    public class FileTypeConfiguration : EntityTypeConfiguration<F_File>
    {
        public FileTypeConfiguration()
        {
            HasKey(model => model.Id);
            Property(model => model.Id)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

        }
    }
}
