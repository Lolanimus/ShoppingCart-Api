using Store.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Store.Tests.ViewModel
{
    public class CartViewModelTest
    {
        [Fact]
        public async Task GetCartByUserId()
        {
            CartViewModel cartViewModel = new() { Id = 1 };
            List<CartViewModel> cartProducts = await cartViewModel.GetCartByUserId();
            Assert.True(cartProducts.Count == 2);
        }

        [Fact]
        public async Task GetCartByUserIdWithProducts()
        {
            CartViewModel cartViewModel = new() { Id = 1 };
            List<CartViewModel> cartProducts = await cartViewModel.GetCartByUserIdWithProducts();
            Assert.Contains("Mens", cartProducts.ElementAt(1).Product.ProductName);
        }
    }
}
