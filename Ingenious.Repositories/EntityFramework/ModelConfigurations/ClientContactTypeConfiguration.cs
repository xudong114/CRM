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
    public class ClientContactTypeConfiguration : EntityTypeConfiguration<ClientContact>
    {
        public ClientContactTypeConfiguration()
        {
            HasKey(model => model.Id);

            Property(model => model.Id)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(model => model.ClientId)
                .IsRequired();
        }
    }
}
