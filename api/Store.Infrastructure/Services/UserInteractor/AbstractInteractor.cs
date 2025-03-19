using Azure.Core;
using Microsoft.AspNetCore.Http;
using Store.Infrastracture.DAL;
using Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Store.Infrastracture.Services.UserInteractor
{
    public abstract class AbstractInteractor : IUserInteractor
    {
        internal readonly CartDAO _dao;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AbstractInteractor(IHttpContextAccessor httpContextAccessor)
        {
            _dao = new CartDAO();
            _httpContextAccessor = httpContextAccessor;
        }

        virtual public List<CartProduct> GetCartByUserId(Guid? Id)
        {
            HttpRequest request = _httpContextAccessor.HttpContext.Request;
            List<CartProduct> cartProducts = new();
            if (request.Cookies.TryGetValue("cart", out string? cartJson) && !string.IsNullOrEmpty(cartJson))
            {
                cartProducts = JsonSerializer.Deserialize<List<CartProduct>>(cartJson)!;
            }

            return cartProducts;
        }
    }
}
