using Healthmed.Appointment.Core.Domain;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using System.Diagnostics.CodeAnalysis;

namespace Healthmed.Appointment.Infrastructure.MongoDb.Serializers
{
    [ExcludeFromCodeCoverage]
    public class PriceSerializer : IBsonSerializer<Price>
    {
        public Type ValueType => typeof(Price);

        public Price Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            var value = context.Reader.ReadDecimal128();
            return (decimal)value;
        }

        public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, Price value)
        {
            context.Writer.WriteDecimal128(value.Amount);
        }

        public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
        {

            if (value is Price price)
                Serialize(context, args, price);
            else
                throw new InvalidOperationException("Value is not a valid price");
        }

        object IBsonSerializer.Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            return Deserialize(context, args);
        }
    }
}
