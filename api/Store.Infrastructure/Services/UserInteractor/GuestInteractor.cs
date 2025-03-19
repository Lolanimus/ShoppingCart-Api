using Store.Infrastracture.DAL;
using Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Infrastracture.Services.UserInteractor
{
    public class GuestInteractor : AbstractInteractor
    {
        public override async Task<List<CartProduct>> GetCartByUserId(Guid? Id)
        {
            List<CartProduct> cartProducts = await base.GetCartByUserId(Id);
            return cartProducts;
        }
    }
}
