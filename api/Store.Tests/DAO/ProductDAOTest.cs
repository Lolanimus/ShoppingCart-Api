using Store.Infrastracture.DAL;
using Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Tests.DAO
{
    public class ProductDAOTest
    {
        [Fact]
        public async Task GetAll()
        {
            ProductDAO dao = new();
            List<Product> selectedProducts = await dao.GetAll();
            Assert.True(selectedProducts.Count == 14);
        }

        [Fact]
        public async Task GetById()
        {
            ProductDAO dao = new();
            Product selectedProducts = await dao.GetById(5);
            Assert.True(selectedProducts.Id == 5);
        }
    }
}
