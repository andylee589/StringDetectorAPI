using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StringDetector.Domain.Migrations;
using System.Data.Entity.Migrations;

namespace StringDetector.API.WebHost
{
    public class EFConfig
    {
        public static void Initialize()
        {

            RunMigrations();
        }

        private static void RunMigrations()
        {

            var efMigrationSettings = new  StringDetector.Domain.Migrations.Configuration ();
            var efMigrator = new DbMigrator(efMigrationSettings);

            efMigrator.Update();
        }
    }
}