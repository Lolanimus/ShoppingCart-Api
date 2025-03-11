using Store.Infrastracture.DAL;
using Store.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Store.ViewModels
{
    public class ProductViewModel
    {
        private readonly ProductDAO _dao;

        public int Id { get; set; }

        public string ProductName { get; set; } = null!;

        public string? ProductGender { get; set; }

        public string ProductImageUri { get; set; } = null!;

        public decimal ProductPrice { get; set; }

        public string? ProductDesc { get; set; }

        public string? TimeStamp { get; set; }

        public ProductViewModel()
        {
            _dao = new ProductDAO();
        }

        public async Task GetById()
        {
            try
            {
                Product product = await _dao.GetById(Id);
                Id = product.Id;
                ProductName = product.ProductName;
                ProductGender = product.ProductGender;
                ProductImageUri = product.ProductImageUri;
                ProductPrice = product.ProductPrice;
                ProductDesc = product.ProductDesc;
                // binary value needs to be stored on client as base64
                TimeStamp = Convert.ToBase64String(product.TimeStamp!);
            }
            catch (NullReferenceException nex)
            {
                Debug.WriteLine(nex.Message);
                ProductDesc = "not found";
            }
            catch (Exception ex)
            {
                ProductDesc = "not found";
                Debug.WriteLine("Call in " + GetType().Name + " " +
                MethodBase.GetCurrentMethod()!.Name + " " + ex.Message);
                throw;
            }
        }

        public async Task<List<ProductViewModel>> GetAll()
        {
            List<ProductViewModel> allVms = new();
            try
            {
                List<Product> allProducts = await _dao.GetAll();
                // we need to convert Student instance to StudentViewModel because
                // the Web Layer isn't aware of the Domain class Student
                foreach (Product product in allProducts)
                {
                    ProductViewModel prodVm = new()
                    {
                        Id = product.Id,
                        ProductName = product.ProductName,
                        ProductGender = product.ProductGender,
                        ProductImageUri = product.ProductImageUri,
                        ProductPrice = product.ProductPrice,
                        ProductDesc = product.ProductDesc,
                        // binary value needs to be stored on client as base64
                        TimeStamp = Convert.ToBase64String(product.TimeStamp!)
                    };
                    allVms.Add(prodVm);
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Problem in " + GetType().Name + " " +
                MethodBase.GetCurrentMethod()!.Name + " " + ex.Message);
                throw;
            }
            return allVms;
        }
    }
}
