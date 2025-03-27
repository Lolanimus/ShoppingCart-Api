using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Store.Infrastracture.DAL;
using Store.Infrastracture.Services.Cookies.CartProducts;
using Store.Infrastracture.Services.Cookies.Token;
using Store.Infrastracture.Services.Cookies;
using Store.Infrastracture.Services.UserInteractor;
using Store.Models;
using Store.Models.Cookies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;
using HttpContextMoq;
using HttpContextMoq.Extensions;
using Microsoft.Extensions.Primitives;
using Microsoft.AspNetCore.Http.Features;
using Store.Infrastracture.Services.Cookies.UserInteractor;
using Store.Infrastracture.Services.Cookies.Authentication;

namespace Store.Tests.Cookies
{
    public class UserInteractorTestFixture
    {
        private readonly string key = "userInfo";
        private readonly ServiceProvider _serviceProvider;
        public IUserInteractor UserInteractor { get; }

        public UserInteractorTestFixture()
        {
            var cookies = new Cookie()
            {
                JwtToken = "",
                CartProducts = new List<CartProduct>()
                {
                    new CartProduct()
                    {
                        ProductId = new Guid("26C0A279-BD48-4D14-B31D-2B303CF7CDDD"),
                        ProductSize = ProductSize.M,
                        Quantity = 1
                    },
                    new CartProduct()
                    {
                        ProductId = new Guid("0F7A693A-8114-4527-9388-340A51DD0A3B"),
                        Quantity = 1
                    }
                }
            };

            var context = new HttpContextMock();

            var request = new Dictionary<string, string>
            {
                { key, JsonSerializer.Serialize(cookies) }
            };

            context.SetupRequestCookies(request);

            var contextAccessor = new HttpContextAccessorMock(context);

            var _services = new ServiceCollection();

            _services.AddSingleton<IHttpContextAccessor>(contextAccessor);
            _services.AddScoped<CookiesService>();
            _services.AddScoped<CartProductsService>();
            _services.AddScoped<TokenService>();
            _services.AddScoped<AuthenticationService>();
            _services.AddScoped<IUserInteractor>(serviceProvider =>
            {
                var authService = serviceProvider.GetRequiredService<AuthenticationService>();
                var cartProductService = serviceProvider.GetRequiredService<CartProductsService>();
                cartProductService.SetCartProducts(cookies.CartProducts);

                return authService.IsUserLoggedIn()
                    ? new RegisteredUserInteractor(cartProductService)
                    : new GuestInteractor(cartProductService);
            });

            _serviceProvider = _services.BuildServiceProvider();

            UserInteractor = _serviceProvider.GetRequiredService<IUserInteractor>();

            context.SetupRequestService(UserInteractor);
        }

        public void Dispose()
        {
            var contextAccessor = _serviceProvider.GetRequiredService<IHttpContextAccessor>();
            contextAccessor.HttpContext.Response.Cookies.Delete(key);

            // Dispose of the service provider
            _serviceProvider.Dispose();
        }
    }

    public class UserInteractorTest : IClassFixture<UserInteractorTestFixture>
    {
        private readonly IUserInteractor _userInteractor;

        public UserInteractorTest(UserInteractorTestFixture classFixture)
        {
            _userInteractor = classFixture.UserInteractor;
        }

        [Fact]
        public async Task GetCartProducts()
        {
            var cartProducts = await _userInteractor.GetCartProducts()!;

            // reads at least smth
            Assert.NotNull(cartProducts);
            // reads the right amount of objects
            Assert.Equal(2, cartProducts.Count);
            // the unspecified ProductSize for a Product with ProductGender == null
            // is automatically assigned a ProductSize.Unknown
            Assert.Equal(ProductSize.Unknown, cartProducts[1].ProductSize);
            
            Assert.True(cartProducts[0].ProductId == new Guid("26C0A279-BD48-4D14-B31D-2B303CF7CDDD"));
        }
    }
}
