﻿using Ingenious.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Repositories.EntityFramework.ModelConfigurations
{
    public class PriceStrategyTypeConfiguration : EntityTypeConfiguration<PriceStrategy>
    {
        public PriceStrategyTypeConfiguration()
        {
            HasKey(model => model.Id);
            Property(model => model.Id)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(model => model.Name)
                .IsRequired();

        }
    }
}