using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using System.Diagnostics.CodeAnalysis;

namespace Healthmed.Appointment.Infrastructure.MongoDb.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class MongoDbConventions
    {
        public static void Register()
        {
            ConventionRegistry.Register("MongoDB Conventions",
                new ConventionPack
                {
                    new IgnoreExtraElementsConvention(true),
                    new CamelCaseElementNameConvention(),
                    new EnumRepresentationConvention(MongoDB.Bson.BsonType.String)
                }, _ => true);

            #pragma warning disable CS0618
            BsonDefaults.GuidRepresentationMode = GuidRepresentationMode.V3;
            #pragma warning restore CS0618
        }
    }
}
