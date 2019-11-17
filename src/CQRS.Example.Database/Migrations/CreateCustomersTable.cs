using FluentMigrator;

namespace CQRS.Example.Database.Migrations
{
    [Migration(1)]
    public class CreateCustomersTable : Migration
    {
        public override void Up()
        {
            Create.Table("Customers")
                 .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                 .WithColumn("Email").AsString(255).Unique("idx_customers_email").NotNullable()
                 .WithColumn("FirstName").AsString(255).NotNullable()
                 .WithColumn("LastName").AsString(255).NotNullable()
                 .WithColumn("Address").AsString(255).NotNullable()
                 .WithColumn("PostalCode").AsString(50).NotNullable()
                 .WithColumn("City").AsString(50).NotNullable()
                 .WithColumn("Country").AsString(50).NotNullable();
        }
        public override void Down()
        {
            Delete.Table("Customers");
        }
    }
}