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
        public async Task GetAllMale()
        {
            ProductDAO dao = new();
            List<Product> selectedProducts = await dao.GetAll(Helper.productGenderConverter.ToEnum("male"));
            Assert.Equal(8, selectedProducts.Count);
        }

        [Fact]
        public async Task GetAllFemale()
        {
            ProductDAO dao = new();
            List<Product> selectedProducts = await dao.GetAll(Helper.productGenderConverter.ToEnum("female"));
            Assert.Equal(11, selectedProducts.Count);
        }

        [Fact]
        public async Task GetAll()
        {
            ProductDAO dao = new();
            List<Product> selectedProducts = await dao.GetAll(Helper.productGenderConverter.ToEnum("maleiugyu"));
            Assert.Empty(selectedProducts);
        }

        [Fact]
        public async Task GetById()
        {
            ProductDAO dao = new();
            Product selectedProducts = await dao.GetById(new Guid("B4E9C133-985A-478A-BCE4-04FF1DD085EF"));
            Assert.True(selectedProducts.Id == new Guid("B4E9C133-985A-478A-BCE4-04FF1DD085EF"));
        }
    }
}
