using Healthmed.Appointment.Core.Domain;
using MongoDB.Bson.Serialization;
using System.Diagnostics.CodeAnalysis;

namespace Healthmed.Appointment.Infrastructure.MongoDb.Serializers
{
    [ExcludeFromCodeCoverage]
    public class AppointmentStatusSerializer : IBsonSerializer<AppointmentStatus>
    {
        public Type ValueType => typeof(AppointmentStatus);

        public AppointmentStatus Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            var value = context.Reader.ReadString();
            return AppointmentStatus.FromName(value);
        }

        public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, AppointmentStatus value)
        {
            context.Writer.WriteString(value.Name);
        }

        public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
        {
            if (value is AppointmentStatus status)
                Serialize(context, args, status);
            else
                throw new NotSupportedException("This is invalid for appointment status");
        }

        object IBsonSerializer.Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            return Deserialize(context, args);
        }
    }
}
