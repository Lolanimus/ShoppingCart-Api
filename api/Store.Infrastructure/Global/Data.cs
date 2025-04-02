using Store.Models;
using Store.Models.Cookies;

namespace Store.Infrastracture.Global
{
    public static class Data
    {
        public static Guid AccessoryCartProductId { get; } = new("B4E9C133-985A-478A-BCE4-04FF1DD085EF");
        public static Guid FemaleCartProductId { get; } = new("AFCED627-F686-4674-B1E2-0926FC3DAC5C");
        public static ProductSize FemaleCartProductSize { get; } = ProductSize.M;
        public static Cookie InitialCookies { get; } = new()
        {
            JwtToken = "",
            CartProducts = new List<CartProduct>()
            {
                new CartProduct()
                {
                    ProductId = Data.FemaleCartProductId,
                    ProductSize = Data.FemaleCartProductSize,
                    Quantity = 2
                },
                new CartProduct()
                {
                    ProductId = Data.AccessoryCartProductId,
                    Quantity = 1
                }
            }
        };
        public static Cookie GlobalCookies { get; set; } = InitialCookies;

        public static void ResetGlobalCookies()
        {
            GlobalCookies.JwtToken = "";
            GlobalCookies.CartProducts!.Clear();

            GlobalCookies.CartProducts.Add(new CartProduct()
            {
                ProductId = Data.FemaleCartProductId,
                ProductSize = Data.FemaleCartProductSize,
                Quantity = 2
            });

            GlobalCookies.CartProducts.Add(new CartProduct()
            {
                ProductId = Data.AccessoryCartProductId,
                Quantity = 1
            });
        }
    }
}
