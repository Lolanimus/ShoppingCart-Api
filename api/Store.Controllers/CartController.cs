using Azure.Core;
using Castle.Components.DictionaryAdapter.Xml;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Store.Infrastracture.DTO;
using Store.Infrastracture.Services.UserInteractor;
using Store.Models;
using Store.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : Controller
    {
        private readonly IUserInteractor _userInteractor;

        public CartController(IUserInteractor userInteractor)
        {
            _userInteractor = userInteractor;
        }

        [HttpGet]
        public IActionResult GetCart(Guid? id = null)
        {
            try
            {
                CartViewModel cartVm = new CartViewModel(_userInteractor) { Id = id };
                List<CartViewModel>? allCartVm = cartVm.GetCart();
                //if(allCartVm.IsNullOrEmpty())
                //    return NotFound();
                return Ok(allCartVm);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Problem in " + GetType().Name + " " +
                MethodBase.GetCurrentMethod()!.Name + " " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError); // something went wrong
            }
        }

        [HttpPost("add")]
        public IActionResult AddToCart(CartProductDTO cartDTO)
        {
            try
            {
                CartViewModel cart = CartViewModel.FromDto(cartDTO, _userInteractor);
                cart.AddCartProduct();
                return cart.Quantity > 0 ? Ok(cart) : BadRequest();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Problem in " + GetType().Name + " " +
                MethodBase.GetCurrentMethod()!.Name + " " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError); // something went wrong
            }
        }

        [HttpDelete("{productId}")]
        public IActionResult DeleteCartProduct(Guid productId)
        {
            try
            {
                CartViewModel cart = new CartViewModel(_userInteractor) { ProductId = productId };
                return cart.DeleteCartProduct() == 1 ? Ok(cart) : BadRequest();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Problem in " + GetType().Name + " " +
                MethodBase.GetCurrentMethod()!.Name + " " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError); // something went wrong
            }
        }

        [HttpDelete]
        public IActionResult ClearCookies()
        {
            try
            {
                _userInteractor.ClearCookies();
                return Ok();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Problem in " + GetType().Name + " " +
                MethodBase.GetCurrentMethod()!.Name + " " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError); // something went wrong
            }
        }
    }
}
