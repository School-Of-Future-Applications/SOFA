namespace SOFA.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using SOFA.Models;
    using System.Collections.Generic;

    internal sealed class Configuration : DbMigrationsConfiguration<SOFA.Models.DBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(SOFA.Models.DBContext context)
        {
        }
    }
}
