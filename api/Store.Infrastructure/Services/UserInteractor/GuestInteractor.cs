using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Store.Infrastracture.DAL;
using Store.Infrastracture.Services.Cookies.CartProducts;
using Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Infrastracture.Services.UserInteractor
{
    public class GuestInteractor : IUserInteractor
    {
        private readonly CartProductsService _cartProductsService;

        public GuestInteractor(CartProductsService cartProductsService)
        {
            _cartProductsService = cartProductsService;
        }        

        virtual public List<CartProduct>? GetCartProducts()
        {
            return _cartProductsService.GetCartProducts();
        }

        virtual public CartProduct GetCartProduct(Guid productId, ProductSize size)
        {
            var cartProducts = GetCartProducts();
            return cartProducts!.Find(prod => prod.ProductId == productId && prod.ProductSize == size)!;
        }

        virtual public int DeleteCartProduct(Guid productId, ProductSize size)
        {
            return _cartProductsService.DeleteCartProduct(GetCartProduct(productId, size));
        }

        virtual public void AddCartProduct(CartProduct cartProduct)
        {
            var cartProducts = _cartProductsService.GetCartProducts()!;
            if (!cartProducts.IsNullOrEmpty())
            {
                if (cartProducts.Exists(prod => prod.ProductId == cartProduct.ProductId && prod.ProductSize == cartProduct.ProductSize))
                {
                    cartProducts.First(prod => prod.ProductId == cartProduct.ProductId).Quantity++;
                    _cartProductsService.SetCartProducts(cartProducts);
                    return;
                }
            }
            _cartProductsService.AddCartProduct(cartProduct);
        }

        virtual public void UpdateCartProduct(CartProduct product)
        {
            _cartProductsService.UpdateCartProduct(product);
        }

        virtual public void ClearCookies()
        {
            _cartProductsService.ClearCookies();
        }
    }
}
