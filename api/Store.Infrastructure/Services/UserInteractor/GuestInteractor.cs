using Microsoft.AspNetCore.Http;
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

        virtual public CartProduct GetCartProduct(Guid id)
        {
            var cartProducts = GetCartProducts();
            return cartProducts.Find(prod => prod.Id == id)!;
        }

        virtual public void DeleteCartProduct(Guid id)
        {
            _cartProductsService.DeleteCartProducts(GetCartProduct(id));
        }

        public void AddCartProduct(CartProduct cartProduct)
        {
            _cartProductsService.AddCartProduct(cartProduct);
        }

        public void UpdateCartProduct(CartProduct product)
        {
            _cartProductsService.UpdatedCartProduct(product);
        }
    }
}
