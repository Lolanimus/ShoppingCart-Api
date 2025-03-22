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

        public Cookie? GetPreviousCookies()
        {
            string? previousJsonCookies = _cookiesService.GetCookies();
            if (previousJsonCookies.IsNullOrEmpty())
                return new Cookie();

            Cookie? previousCookies = JsonSerializer.Deserialize<Cookie>(previousJsonCookies!);
    
            return previousCookies!;
        }

        public List<CartProduct>? GetCartProducts()
        {
            string? cookies = _cookiesService.GetCookies();

            if (cookies.IsNullOrEmpty())
                return null;

            Cookie? cartProducts = JsonSerializer.Deserialize<Cookie>(cookies!);

            return cartProducts!.CartProducts;
        }

        public void SetCartProducts(List<CartProduct> cartProducts)
        {
            var prevCookies = GetPreviousCookies();
            prevCookies!.CartProducts = cartProducts;
            string? updatedJsonCookies = JsonSerializer.Serialize(prevCookies);

            _cookiesService.UpdateCookies(updatedJsonCookies);
        }

        public void DeleteCartProducts(CartProduct cartProduct)
        {
            var prevCookie = GetPreviousCookies();
            var prevCartProducts = prevCookie.CartProducts;
            var prodToRemove = (CartProduct)prevCartProducts!.Where(prod => prod.Id == cartProduct.Id);
            prevCartProducts!.Remove(prodToRemove);
            prevCookie.CartProducts = prevCartProducts;
            string prevJsonCookie = JsonSerializer.Serialize(prevCookie);

            _cookiesService.UpdateCookies(prevJsonCookie);
        }

        public void AddCartProduct(CartProduct cartProduct)
        {
            var prevCookie = GetPreviousCookies();
            var prevCartProducts = prevCookie.CartProducts;
            prevCartProducts!.Add(cartProduct);
            prevCookie.CartProducts = prevCartProducts;
            string prevJsonCookie = JsonSerializer.Serialize(prevCookie);

            _cookiesService.UpdateCookies(prevJsonCookie);
        }

        public void UpdatedCartProduct(CartProduct cartProduct)
        {
            DeleteCartProducts(cartProduct);
            AddCartProduct(cartProduct);
        }
    }
}
