using Store.Infrastracture.DAL;
using Store.Infrastracture.Helpers;
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
            List<Product> selectedProducts = await dao.GetAll(Helper.productGenderConverter.ToEnum("male"));
            Assert.True(selectedProducts.Count == 6);
        }

        [Fact]
        public async Task GetById()
        {
            ProductDAO dao = new();
            Product selectedProducts = await dao.GetById(new Guid("26C0A279-BD48-4D14-B31D-2B303CF7CDDD"));
            Assert.True(selectedProducts.Id == new Guid("26C0A279-BD48-4D14-B31D-2B303CF7CDDD"));
        }
    }
}
