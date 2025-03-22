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
    public class UserInteractor : GuestInteractor
    {
        public UserInteractor(CartProductsService cartProductsService) : base(cartProductsService)
        {
        }


    }
}