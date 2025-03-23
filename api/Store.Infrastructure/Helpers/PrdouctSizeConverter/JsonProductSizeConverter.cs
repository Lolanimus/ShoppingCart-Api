using Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Store.Infrastracture.Helpers.PrdouctSizeConverter
{
    public class JsonProductSizeConverter : JsonConverter<ProductSize>
    {
        public override ProductSize Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string? value = reader.GetString()?.ToLower();

            return ProductSizeConverter.ToEnum(value!);
        }

        public override void Write(Utf8JsonWriter writer, ProductSize value, JsonSerializerOptions options)
        {
            string size = ProductSizeConverter.FromEnum(value);

            writer.WriteStringValue(size);
        }
    }
}
