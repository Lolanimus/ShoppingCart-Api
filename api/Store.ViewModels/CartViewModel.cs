﻿using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Store.Infrastracture.DAL;
using Store.Infrastracture.DTO;
using Store.Infrastracture.Helpers;
using Store.Infrastracture.Helpers.PrdouctSizeConverter;
using Store.Infrastracture.Services.Cookies;
using Store.Infrastracture.Services.UserInteractor;
using Store.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Store.ViewModels
{
    public class CartViewModel
    {
        private IUserInteractor _userInteractor;

        public Guid? Id { get; set; }

        public Guid ProductId { get; set; }

        public int? Quantity { get; set; }

        public string? ProductSize { get; set; }

        public virtual Product? Product { get; set; } = null!;

        public virtual WebUser? User { get; set; } = null!;

        public CartViewModel(IUserInteractor userInteractor)
        {
            _userInteractor = userInteractor;
        }

        public static CartViewModel FromDto(CartProductDTO dto, IUserInteractor userInteractor)
        {
            return new CartViewModel(userInteractor)
            {
                Id = dto.Id,
                ProductId = dto.ProductId,
                ProductSize = Helper.sizeConverter.FromEnum(dto.ProductSize),
                Quantity = dto.Quantity
            };
        }

        public async Task<List<CartViewModel>>? GetCart()
        {
            List<CartViewModel> allVms = new();
            try
            {
                List<CartProduct>? allCartProducts = await _userInteractor.GetCartProducts()!;

                if (allCartProducts.IsNullOrEmpty())
                    return null!;

                foreach (CartProduct? cartProduct in allCartProducts)
                {
                    CartViewModel? cartVm = new(_userInteractor)
                    {
                        Id = cartProduct.Id,
                        ProductId = cartProduct.ProductId,
                        ProductSize = Helper.sizeConverter.FromEnum(cartProduct.ProductSize),
                        Quantity = cartProduct.Quantity,
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

        public async Task<int> AddCartProduct()
        {
            try
            {
                CartProduct? cartProduct = new CartProduct()
                {
                    Id = Id,
                    ProductId = ProductId,
                    ProductSize = Helper.sizeConverter.ToEnum(ProductSize!),
                    Quantity = Quantity ?? 1,
                };

                return await _userInteractor.AddCartProduct(cartProduct);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Problem in " + GetType().Name + " " +
                MethodBase.GetCurrentMethod()!.Name + " " + ex.Message);
                throw;
            }
        }

        public async Task<int> DeleteCartProduct()
        {
            try
            {
                return await _userInteractor.DeleteCartProduct(ProductId, Helper.sizeConverter.ToEnum(ProductSize!));
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Problem in " + GetType().Name + " " +
                MethodBase.GetCurrentMethod()!.Name + " " + ex.Message);
                throw;
            }
        }

        public void ClearCookies()
        {
            try
            {
                _userInteractor.ClearCookies();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Problem in " + GetType().Name + " " +
                MethodBase.GetCurrentMethod()!.Name + " " + ex.Message);
                throw;
            }

        }
    }
}
