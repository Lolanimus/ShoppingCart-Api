using Microsoft.AspNetCore.Mvc;
using Store.Models;
using Store.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Tests.ViewModel
{
    public class ProductViewModelTest
    {
        [Fact]
        public async Task GetAllMale()
        {
            ProductViewModel prodVm = new() { ProductGender = "male" };
            List<ProductViewModel> products = await prodVm.GetAll();
            Assert.Equal(8, products.Count);
        }

        [Fact]
        public async Task GetAllFemale()
        {
            ProductViewModel prodVm = new() { ProductGender = "female" };
            List<ProductViewModel> products = await prodVm.GetAll();
            Assert.Equal(11, products.Count);
        }

        [Fact]
        public async Task GetAllUnknownGender()
        {
            ProductViewModel prodVm = new() { ProductGender = "wtfIsThis" };
            List<ProductViewModel> products = await prodVm.GetAll();
            Assert.Empty(products);
        }

        [Fact]
        public async Task GetById()
        {
            ProductViewModel prodVm = new() { Id = new Guid("B4E9C133-985A-478A-BCE4-04FF1DD085EF") };
            Product product = await prodVm.GetById();
            Assert.NotNull(product);
        }
    }
}
