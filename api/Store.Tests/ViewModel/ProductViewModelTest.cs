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
        public async Task GetAll()
        {
            ProductViewModel prodVm = new();
            List<ProductViewModel> products = await prodVm.GetAll();
            Assert.True(products.Count == 14);
        }

        [Fact]
        public async Task GetById()
        {
            ProductViewModel prodVm = new() { Id = new Guid("26C0A279-BD48-4D14-B31D-2B303CF7CDDD") };
            Product product = await prodVm.GetById();
            Assert.NotNull(product);
        }
    }
}
