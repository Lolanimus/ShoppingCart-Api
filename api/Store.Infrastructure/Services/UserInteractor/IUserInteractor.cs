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
        List<CartProduct> GetCartByUserId(Guid? Id);
    }
}
