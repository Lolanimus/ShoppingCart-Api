using Azure.Core;
using Castle.Components.DictionaryAdapter.Xml;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
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
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserInteractor _userInteractor;

        public CartController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _userInteractor = _httpContextAccessor.HttpContext.Request.Cookies["userInfo"].IsNullOrEmpty() 
                ? new GuestInteractor(_httpContextAccessor) 
                : new UserInteractor(_httpContextAccessor);
        }

        [HttpGet]
        public async Task<IActionResult> GetCartByUserId(Guid? id = null)
        {
            try
            {
                CartViewModel prodVm = new CartViewModel(_userInteractor) { Id = id };
                await prodVm.GetCartByUserId();
                return Ok(prodVm);
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
