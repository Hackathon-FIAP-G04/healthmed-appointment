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
            BsonSerializer.TryRegisterSerializer(new AppointmentStatusSerializer());
            BsonSerializer.TryRegisterSerializer(new DurationSerializer());
            BsonSerializer.TryRegisterSerializer(new PeriodSerializer());
            BsonSerializer.TryRegisterSerializer(new PriceSerializer());
        }
    }
}
