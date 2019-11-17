namespace CQRS.Example.Database.Migrations
{
    public interface IDatabaseMigrator
    {
        void Migrate();
    }
}