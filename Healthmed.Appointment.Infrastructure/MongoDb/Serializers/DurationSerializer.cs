using Healthmed.Appointment.Core.Domain;
using MongoDB.Bson.Serialization;
using System.Diagnostics.CodeAnalysis;

namespace Healthmed.Appointment.Infrastructure.MongoDb.Serializers
{
    [ExcludeFromCodeCoverage]
    public class DurationSerializer : IBsonSerializer<Duration>
    {
        public Type ValueType => typeof(Duration);

        public Duration Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            var value = context.Reader.ReadInt32();
            return value;
        }

        public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, Duration value)
        {
            context.Writer.WriteInt32(value.Minutes);
        }

        public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
        {
            if (value is Duration duration)
                Serialize(context, args, duration);
            else
                throw new NotImplementedException("This is invalid duration value");
        }

        object IBsonSerializer.Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            return Deserialize(context, args);
        }
    }
}
