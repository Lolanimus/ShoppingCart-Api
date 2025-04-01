using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Store.Infrastracture.Services.Cookies.CartProducts;
using Store.Infrastracture.Services.Cookies.Token;
using Store.Infrastracture.Services.Cookies;
using Store.Models;
using Store.Models.Cookies;
using System.Text.Json;
using Store.Infrastracture.Services.Cookies.UserInteractor;
using Store.Infrastracture.Services.Cookies.Authentication;
using Moq;
using System.Text;


namespace Store.Tests.Cookies
{
    public static class Data
    {
        public static Guid AccessoryCartProductId { get; } = new("0F7A693A-8114-4527-9388-340A51DD0A3B");
        public static Guid FemaleCartProductId { get; } = new("26C0A279-BD48-4D14-B31D-2B303CF7CDDD");
        public static ProductSize FemaleCartProductSize { get; } = ProductSize.M;
        public static Cookie InitialCookies { get; } = new()
        {
            JwtToken = "",
            CartProducts = new List<CartProduct>()
            {
                new CartProduct()
                {
                    ProductId = Data.FemaleCartProductId,
                    ProductSize = Data.FemaleCartProductSize,
                    Quantity = 2
                },
                new CartProduct()
                {
                    ProductId = Data.AccessoryCartProductId,
                    Quantity = 1
                }
            }
        };
        public static Cookie GlobalCookies { get; set; } = InitialCookies;
    }

    public class UserInteractorTestFixture
    {
        
        private readonly string key = "userInfo";
        private readonly ServiceProvider _serviceProvider;
        public IUserInteractor UserInteractor { get; }

        private void Append(string key, string value, CookieOptions options)
        {
            Cookie valueCookies = JsonSerializer.Deserialize<Cookie>(value)!;
            Data.GlobalCookies.JwtToken = valueCookies.JwtToken ?? "";
            Data.GlobalCookies.CartProducts!.Clear();
            foreach (CartProduct cartProduct in valueCookies.CartProducts!)
            {
                Data.GlobalCookies.CartProducts.Add(cartProduct);
            }
        }

        private void Delete(string key)
        {
            Data.GlobalCookies.JwtToken = "";
            Data.GlobalCookies.CartProducts!.Clear();
        }

        public UserInteractorTestFixture()
        { 
            var jsonCookies = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(Data.GlobalCookies));
            var context = new Mock<HttpContext>();
            var response = new Mock<HttpResponse>();
            var request = new Mock<HttpRequest>();

            var requestCookie = new Mock<IRequestCookieCollection>();

            requestCookie.Setup(rc => rc[key]).Returns(JsonSerializer.Serialize(Data.GlobalCookies));

            var responseCookie = new Mock<IResponseCookies>();
            responseCookie.Setup(cxt => cxt.Append(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CookieOptions>()))
                .Callback<string, string, CookieOptions>((key, value, opts) =>
                {
                    Append(key, value, opts);
                    requestCookie.Setup(rc => rc[key]).Returns(JsonSerializer.Serialize(Data.GlobalCookies));
                });
            responseCookie.Setup(cxt => cxt.Delete(It.IsAny<string>()))
                .Callback<string>((key) =>
                {
                    Delete(key);
                    requestCookie.Setup(rc => rc[key]).Returns(JsonSerializer.Serialize(Data.GlobalCookies));
                });

            response.Setup(r => r.Cookies).Returns(responseCookie.Object);
            request.Setup(r => r.Cookies).Returns(requestCookie.Object);
            context.Setup(r => r.Response).Returns(response.Object);
            context.Setup(r => r.Request).Returns(request.Object);

            var contextAccessor = new Mock<IHttpContextAccessor>();
            contextAccessor.Setup(c => c.HttpContext).Returns(context.Object);

            var _services = new ServiceCollection();

            _services.AddSingleton<IHttpContextAccessor>(contextAccessor.Object);
            _services.AddScoped<CookiesService>();
            _services.AddScoped<CartProductsService>();
            _services.AddScoped<TokenService>();
            _services.AddScoped<AuthenticationService>();
            _services.AddScoped<IUserInteractor>(serviceProvider =>
            {
                var authService = serviceProvider.GetRequiredService<AuthenticationService>();
                var cartProductService = serviceProvider.GetRequiredService<CartProductsService>();

                return authService.IsUserLoggedIn()
                    ? new RegisteredUserInteractor(cartProductService)
                    : new GuestInteractor(cartProductService);
            });

            _serviceProvider = _services.BuildServiceProvider();

            UserInteractor = _serviceProvider.GetRequiredService<IUserInteractor>();
        }
    }
   
    public class UserInteractorTest : IClassFixture<UserInteractorTestFixture>
    {
        private readonly IUserInteractor _inter;
        private UserInteractorTestFixture _classFixture;

        public UserInteractorTest(UserInteractorTestFixture classFixture)
        {
            _classFixture = classFixture;
            _inter = _classFixture.UserInteractor;
        }

        [Fact]
        public async Task RunGetTests()
        {
            var getTests = new Get(_inter);
            await getTests.GetCartProducts();
            await getTests.GetCartProduct();
        }

        [Fact]
        public async Task RunDeleteTests()
        {
            var deleteTests = new Delete(_inter);
            await deleteTests.DeleteOneQuantityCartProduct();
            await deleteTests.DeleteAllQuantityCartProduct();
        }

        [Fact]
        public async Task RunPostTests()
        {
            var postTests = new Post(_inter);

        }

        public class Get
        {
            private readonly IUserInteractor _inter;

            public Get(IUserInteractor inter)
            {
                _inter = inter;
            }

            public async Task GetCartProducts()
            {
                var cartProducts = await _inter.GetCartProducts()!;

                // reads at least smth
                Assert.NotNull(cartProducts);
                // reads the right amount of objects
                Assert.Equal(2, cartProducts.Count);
                // the unspecified ProductSize for a Product with ProductGender == null
                // is automatically assigned a ProductSize.Unknown
                Assert.Equal(ProductSize.Unknown, cartProducts[1].ProductSize);
                Assert.Null(cartProducts[1].Product!.ProductGender);

                Assert.Equal(Data.FemaleCartProductSize, cartProducts[0].ProductSize);
                Assert.Equal("Female", cartProducts[0].Product!.ProductGender);

                Assert.True(cartProducts[0].ProductId == Data.FemaleCartProductId);
            }

            public async Task GetCartProduct()
            {
                var cartProduct = await _inter.GetCartProduct(
                    Data.FemaleCartProductId,
                    ProductSize.M
                );

                Assert.NotNull(cartProduct);
                Assert.Equal(Data.FemaleCartProductId, cartProduct.ProductId);
                Assert.Equal("Female", cartProduct.Product!.ProductGender);
            }
        }


        public class Delete
        {
            private readonly IUserInteractor _inter;

            public Delete(IUserInteractor inter)
            {
                _inter = inter;
            }

            public async Task DeleteOneQuantityCartProduct()
            {
                var cartProducts = await _inter.GetCartProducts()!;
                Assert.Equal(2, cartProducts[0].Quantity);
                await _inter.DeleteCartProduct(Data.FemaleCartProductId, Data.FemaleCartProductSize);
                cartProducts = await _inter.GetCartProducts()!;
                Assert.True(cartProducts.Count == 2);
                Assert.Equal(1, cartProducts[0].Quantity);
            }

            public async Task DeleteAllQuantityCartProduct()
            {
                var cartProducts = await _inter.GetCartProducts()!;
                Assert.Equal(1, cartProducts[0].Quantity);
                await _inter.DeleteCartProduct(Data.FemaleCartProductId, Data.FemaleCartProductSize, false);
                cartProducts = await _inter.GetCartProducts()!;
                Assert.True(cartProducts.Count == 1);
                Assert.True(cartProducts[0].ProductId == Data.AccessoryCartProductId);
            }
        }

        public class Post
        {
            private IUserInteractor _inter;

            public CartProduct ProductToAdd { get; } = new CartProduct()
            {
                ProductId = new Guid("F96308A1-369E-4FF3-B338-4F034E648FC8"),
                ProductSize = ProductSize.L
            };

            public CartProduct ProductQuantityIncrement { get; } = new CartProduct()
            {
                ProductId = Data.FemaleCartProductId,
                ProductSize = Data.FemaleCartProductSize
            };

            public Post(IUserInteractor inter)
            {
                _inter = inter;
            }

            public async Task AddNewCartProduct()
            {
                await _inter.AddCartProduct(ProductToAdd);
                var cartProducts = await _inter.GetCartProducts()!;
                var cartProduct = await _inter.GetCartProduct(new Guid("F96308A1-369E-4FF3-B338-4F034E648FC8"), ProductSize.L);
                Assert.NotNull(cartProduct);
                Assert.Equal(3, cartProducts.Count);
            }

            public async Task IncrementCartProductQuantity()
            {
                await _inter.AddCartProduct(ProductQuantityIncrement);
                var cartProducts = await _inter.GetCartProducts()!;
                var cartProduct = await _inter.GetCartProduct(Data.FemaleCartProductId, Data.FemaleCartProductSize);
                Assert.NotNull(cartProduct);
                Assert.Equal(3, cartProduct.Quantity);
                Assert.Equal(2, cartProducts.Count);
            }
        }
    }
}
