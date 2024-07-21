using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using System.Diagnostics.CodeAnalysis;

namespace Healthmed.Appointment.Infrastructure.MongoDb.Serializers
{
    [ExcludeFromCodeCoverage]
    public static class MongoDbSerializers
    {
        public static void Register()
        {
            BsonSerializer.TryRegisterSerializer(new ObjectSerializer(ObjectSerializer.AllAllowedTypes));
            BsonSerializer.TryRegisterSerializer(GuidSerializer.StandardInstance);
        }
    }
}
