using Store.Infrastracture.DAL;
using Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Tests.DAO
{
    public class CartDAOTest
    {
        [Fact]
        public async Task GetCartByUserId()
        {
            CartDAO dao = new();
            List<CartProduct> cart = await dao.GetCartByUserId(1);
            Assert.True(cart.Count == 2);
        }

        [Fact]
        public async Task GetCartByUserIdWithProducts()
        {
            CartDAO dao = new();
            List<CartProduct> cart = await dao.GetCartByUserIdWithProducts(1);
            Assert.True(cart.ElementAt(1).ProductId == 5);
        }

        [Fact]
        public async Task AddProductCart()
        {
            CartDAO dao = new();
            await dao.AddProductCart();
        }
    }
}
