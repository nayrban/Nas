namespace NasData.NasModelContextMigrations
{
    using MySql.Data.Entity;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Migrations.Model;
    using System.Data.Entity.Migrations.Sql;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<NasData.NasModelContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"NasModelContextMigrations";
            SetSqlGenerator("MySql.Data.MySqlClient", new CustomMySqlMigrationSqlGenerator());
        }

        protected override void Seed(NasData.NasModelContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }

    internal class CustomMySqlMigrationSqlGenerator : MySqlMigrationSqlGenerator
    {
        protected override MigrationStatement Generate(AddColumnOperation addColumnOperation)
        {            

            return base.Generate(addColumnOperation);
        }

        protected override MigrationStatement Generate(CreateTableOperation createTableOperation)
        {           

            return base.Generate(createTableOperation);
        }
    }
}
