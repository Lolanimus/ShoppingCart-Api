using Store.Infrastracture.Helpers.PrdouctSizeConverter;
using Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Infrastracture.Helpers
{
    public static class Helper
    {
        public static JsonProductSizeConverter jsonProductSizeConverter = new();
        public static ProductSizeConverter sizeConverter = new();
        public static bool ClothesHasSize(CartProduct prod)
        {
            if (prod.Product!.ProductGender != null && prod.ProductSize == ProductSize.Unknown)
                return false;
            else
                return true;
        }
    }
}
