using Microsoft.IdentityModel.Tokens;
using Store.Infrastracture.DAL;
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
        public override async Task<List<CartProduct>> GetCartByUserId(Guid? Id)
        {
            List<CartProduct> cartProducts = await base.GetCartByUserId(Id);
            if (cartProducts.IsNullOrEmpty())
            {
                // get cart redis cache, and if not null populate cookies with it
                // else return 404
            }
            // put cartProducts into redis cache
            return cartProducts;
        }
    }
}