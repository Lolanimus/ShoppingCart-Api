using Microsoft.AspNetCore.Http;
using Store.Infrastracture.DAL;
using Store.Infrastracture.Services.UserInteractor;
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
    public class CartViewModel
    {
        private readonly IUserInteractor _userInteractor;

        public Guid? Id { get; set; }

        public Guid ProductId { get; set; }

        public int Quantity { get; set; }

        public string? ProductSize { get; set; }

        public string? TimeStamp { get; set; }

        public virtual Product Product { get; set; } = null!;

        public virtual WebUser User { get; set; } = null!;

        public CartViewModel(IUserInteractor userInteractor)
        {
            _userInteractor = userInteractor;
        }

        public async Task<List<CartViewModel>> GetCartByUserId()
        {
            List<CartViewModel> allVms = new();
            try
            {
                List<CartProduct> allCartProducts = await _userInteractor.GetCartByUserId(Id);
                foreach (CartProduct cartProduct in allCartProducts)
                {
                    CartViewModel cartVm = new(_userInteractor)
                    {
                        Id = cartProduct.Id,
                        ProductId = cartProduct.ProductId,
                        ProductSize = cartProduct.ProductSize,
                        Quantity = cartProduct.Quantity,
                        TimeStamp = Convert.ToBase64String(cartProduct.TimeStamp!)
                    };
                    allVms.Add(cartVm);
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

        //public async Task<List<CartViewModel>> GetCartByUserIdWithProducts()
        //{
        //    List<CartViewModel> allVms = new();
        //    try
        //    {
        //        List<CartProduct> allCartProducts = await dao.GetCartByUserIdWithProducts(Id);
        //        // we need to convert Student instance to StudentViewModel because
        //        // the Web Layer isn't aware of the Domain class Student
        //        foreach (CartProduct cartProduct in allCartProducts)
        //        {
        //            CartViewModel cartVm = new(_userInteractor)
        //            {
        //                Id = cartProduct.Id,
        //                ProductId = cartProduct.ProductId,
        //                ProductSize = cartProduct.ProductSize,
        //                Quantity = cartProduct.Quantity,
        //                Product = cartProduct.Product,
        //                // binary value needs to be stored on client as base64
        //                TimeStamp = Convert.ToBase64String(cartProduct.TimeStamp!)
        //            };
        //            allVms.Add(cartVm);
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine("Problem in " + GetType().Name + " " +
        //        MethodBase.GetCurrentMethod()!.Name + " " + ex.Message);
        //        throw;
        //    }
        //    return allVms;
        //}
    }
}
