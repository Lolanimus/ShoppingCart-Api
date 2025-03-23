using Microsoft.IdentityModel.Tokens;
using Store.Infrastracture.Services.Cookies;
using Store.Models;
using Store.Models.Cookies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Store.Infrastracture.Services.Cookies.CartProducts
{
    public class CartProductsService
    {
        private readonly CookiesService _cookiesService;

        public CartProductsService(CookiesService cookiesService)
        {
            _cookiesService = cookiesService;
        }

        public void ClearCookies()
        {
            _cookiesService.ClearCookies();
        }

        public Cookie? GetPreviousCookies()
        {
            string? previousJsonCookies = _cookiesService.GetCookies();
            if (previousJsonCookies.IsNullOrEmpty())
                return new Cookie();

            Cookie? previousCookies = JsonSerializer.Deserialize<Cookie>(previousJsonCookies!, DeserializerOptions.opts);
    
            return previousCookies!;
        }

        public List<CartProduct>? GetCartProducts()
        {
            string? cookies = _cookiesService.GetCookies();

            if (cookies.IsNullOrEmpty())
                return null;

            Cookie? cartProducts = JsonSerializer.Deserialize<Cookie>(cookies!, DeserializerOptions.opts);

            return cartProducts!.CartProducts;
        }

        public void SetCartProducts(List<CartProduct> cartProducts)
        {
            var prevCookies = GetPreviousCookies();
            prevCookies!.CartProducts = cartProducts;
            string? updatedJsonCookies = JsonSerializer.Serialize(prevCookies, DeserializerOptions.opts);

            _cookiesService.UpdateCookies(updatedJsonCookies);
        }

        public int DeleteCartProduct(CartProduct cartProduct)
        {
            var prevCookie = GetPreviousCookies()!;
            var prevCartProducts = prevCookie.CartProducts!;
            CartProduct prodToRemove = prevCartProducts.FirstOrDefault(prod => prod.Id == cartProduct.Id)!;
            prevCartProducts!.Remove(prodToRemove);
            prevCookie.CartProducts = prevCartProducts;
            string prevJsonCookie = JsonSerializer.Serialize(prevCookie, DeserializerOptions.opts);

            _cookiesService.UpdateCookies(prevJsonCookie);

            if (!prevCookie.CartProducts!.Any(prod => prod.ProductId == cartProduct.ProductId))
                return 1;
            else
                return -1;
        }

        public void AddCartProduct(CartProduct cartProduct)
        {
            var prevCookie = GetPreviousCookies();
            var prevCartProducts = prevCookie.CartProducts;
            prevCartProducts!.Add(cartProduct);
            prevCookie.CartProducts = prevCartProducts;
            string prevJsonCookie = JsonSerializer.Serialize(prevCookie, DeserializerOptions.opts);

            _cookiesService.UpdateCookies(prevJsonCookie);
        }

        public void UpdateCartProduct(CartProduct cartProduct)
        {
            DeleteCartProduct(cartProduct);
            AddCartProduct(cartProduct);
        }
    }
}
