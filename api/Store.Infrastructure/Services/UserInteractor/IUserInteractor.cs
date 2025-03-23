using Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Infrastracture.Services.UserInteractor
{
    public interface IUserInteractor
    {
        List<CartProduct> GetCartProducts();

        CartProduct GetCartProduct(Guid productId);

        int DeleteCartProduct(Guid productId);

        void AddCartProduct(CartProduct cartProduct);

        void UpdateCartProduct(CartProduct cartProduct);

        void ClearCookies();
    }
}
