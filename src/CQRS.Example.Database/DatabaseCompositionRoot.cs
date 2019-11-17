using System.Data;
using System.Data.SQLite;
using CQRS.Example.Database.Migrations;
using CQRS.Example.Shared;
using CQRS.Transactions;
using LightInject;
using ResourceReader;

namespace CQRS.Example.Database
{
    public class DatabaseCompositionRoot : ICompositionRoot
    {
        public void Compose(IServiceRegistry serviceRegistry)
        {
            serviceRegistry
                .RegisterScoped<IDbConnection>(CreateConnection)
                .Decorate<IDbConnection, ConnectionDecorator>()
                .RegisterSingleton<ISqlProvider>(f => new ResourceBuilder().Build<ISqlProvider>())
                .RegisterSingleton<IDatabaseMigrator, DatabaseMigrator>();
        }

        private static IDbConnection CreateConnection(IServiceFactory serviceFactory)
        {
            var configuration = serviceFactory.GetInstance<ApplicationConfiguration>();
            SQLiteConnection connection = new SQLiteConnection(configuration.ConnectionString);
            connection.Open();
            return connection;
        }
    }
}