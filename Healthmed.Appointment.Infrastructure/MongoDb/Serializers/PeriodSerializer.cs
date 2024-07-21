using Healthmed.Appointment.Core.Domain;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using System.Diagnostics.CodeAnalysis;

namespace Healthmed.Appointment.Infrastructure.MongoDb.Serializers
{
    [ExcludeFromCodeCoverage]
    public class PeriodSerializer : IBsonSerializer<Period>
    {
        public Type ValueType => typeof(Period);

        public Period Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            context.Reader.ReadStartDocument();
            var startHour = context.Reader.ReadInt32();
            var startMinute = context.Reader.ReadInt32();
            var endHour = context.Reader.ReadInt32();
            var endMinute = context.Reader.ReadInt32();
            context.Reader.ReadEndDocument();

            return new(startHour, startMinute, endHour, endMinute);
        }

        public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, Period value)
        {
            
            context.Writer.WriteStartDocument();
            context.Writer.WriteInt32("startHour", value.StartHour);
            context.Writer.WriteInt32("startMinute", value.StartMinute);
            context.Writer.WriteInt32("endHour", value.EndHour);
            context.Writer.WriteInt32("endMinute", value.EndMinute);
            context.Writer.WriteEndDocument();
        }

        public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
        {
            if (value is Period period)
                Serialize(context, args, period);
            else
                throw new InvalidOperationException("Value is not valid period");
        }

        object IBsonSerializer.Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            return Deserialize(context, args);
        }
    }
}
