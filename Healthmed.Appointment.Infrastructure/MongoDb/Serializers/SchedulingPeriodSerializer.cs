using Healthmed.Appointment.Core.Domain;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using System.Diagnostics.CodeAnalysis;

namespace Healthmed.Appointment.Infrastructure.MongoDb.Serializers
{
    [ExcludeFromCodeCoverage]
    public class SchedulingPeriodSerializer : IBsonSerializer<SchedulingPeriod>
    {
        public Type ValueType => typeof(SchedulingPeriod);

        public SchedulingPeriod Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            context.Reader.ReadStartDocument();
            var startTime = context.Reader.ReadDateTime();
            var endTime = context.Reader.ReadDateTime();
            context.Reader.ReadEndDocument();
            return new SchedulingPeriod(DateTime.FromBinary(startTime), DateTime.FromBinary(endTime));
        }

        public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, SchedulingPeriod value)
        {
            context.Writer.WriteStartDocument();
            context.Writer.WriteDateTime("startTime", value.StartTime.Ticks);
            context.Writer.WriteDateTime("endTIme", value.EndTime.Ticks);
            context.Writer.WriteEndDocument();
        }

        public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
        {
            if (value is SchedulingPeriod period)
                Serialize(context, args, period);
            else
                throw new InvalidOperationException("The value is invalid for scheduling period");
        }

        object IBsonSerializer.Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            return Deserialize(context, args);
        }
    }
}
