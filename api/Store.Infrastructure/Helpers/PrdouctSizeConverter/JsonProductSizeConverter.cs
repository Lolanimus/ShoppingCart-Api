using Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Store.Infrastracture.Helpers;

namespace Store.Infrastracture.Helpers.PrdouctSizeConverter
{
    public class JsonProductSizeConverter : JsonConverter<ProductSize>
    {
        public override ProductSize Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if(reader.TokenType == JsonTokenType.Number)
                return Helper.sizeConverter.ToEnum(Helper.sizeConverter.FromIntToStr(reader.GetInt32()));
            else
                return Helper.sizeConverter.ToEnum(reader.GetString()!.ToLower());
        }

        public override void Write(Utf8JsonWriter writer, ProductSize value, JsonSerializerOptions options)
        {
            string size = Helper.sizeConverter.FromEnum(value);

            writer.WriteStringValue(size);
        }
    }
}
