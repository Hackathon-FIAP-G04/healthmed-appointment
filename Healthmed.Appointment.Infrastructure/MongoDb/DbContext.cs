using Healthmed.Appointment.Infrastructure.MongoDb.Configurations;
using MongoDB.Driver;
using MongoDB.Driver.Core.Extensions.DiagnosticSources;

namespace Healthmed.Appointment.Infrastructure.MongoDb
{
    public interface IDbContext
    {
        IMongoClient Client { get; }
        IMongoDatabase Database { get; }
    }

    public class DbContext : IDbContext
    {
        public IMongoClient Client { get; }

        public IMongoDatabase Database { get; }

        public DbContext(MongoDbConfiguration configuration)
        {
            var url = new MongoUrl(configuration.ConnectionString);
            var settings = MongoClientSettings.FromUrl(url);
            var options = new InstrumentationOptions { CaptureCommandText = true };

            settings.ClusterConfigurator = cb => cb.Subscribe(new DiagnosticsActivityEventSubscriber(options));

            Client = new MongoClient(settings);
            Database = Client.GetDatabase(configuration.Database);
        }
    }
}
