using Ingenious.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace Ingenious.Repositories.EntityFramework
{
    public sealed class IngeniousDbContextInitializer : DropCreateDatabaseIfModelChanges<IngeniousDbContext>
    {
        public static void InitializeDatabase()
        {
            Database.SetInitializer<IngeniousDbContext>(new MigrateDatabaseToLatestVersion<IngeniousDbContext, IngeniousDbMigrationsConfiguration>());
            using (var db = new IngeniousDbContext())
            {
                db.Database.Initialize(true);
            }
        }

        protected override void Seed(IngeniousDbContext context)
        {
            base.Seed(context);
        }

    }

}
