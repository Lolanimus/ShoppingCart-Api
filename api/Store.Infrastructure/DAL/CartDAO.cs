using Store.Infrastracture.Repository;
using Store.Models;
using Store.UseCases.Interfaces.Repository;
using System.Diagnostics;
using System.Reflection;
using System.Data.Entity;
using Store.Infrastracture.Services.UserInteractor;
using Microsoft.AspNetCore.Http;

namespace Store.Infrastracture.DAL
{
    public class CartDAO
    {
        private readonly IRepository<CartProduct> repo;
        
        public CartDAO() 
        {
            repo = new StoreRepository<CartProduct>();
        }

        public async Task<List<CartProduct>> GetCartByUserId(Guid? userId)
        {
            List<CartProduct>? cartProducts;
            try
            {
                StoreContext _db = new();
                // userId == cartId
                cartProducts = await repo.GetSome(cart => cart.Id == userId);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Problem in " + GetType().Name + " " +
                MethodBase.GetCurrentMethod()!.Name + " " + ex.Message);
                throw;
            }
            return cartProducts!;
        }

        public async Task<List<CartProduct>> GetCartByUserIdWithProducts(Guid? userId)
        {
            List<CartProduct> selectedCart = await GetCartByUserId(userId);
            List<CartProduct>? fullCartProducts;
            try
            {
                fullCartProducts = selectedCart.AsQueryable().Include(c => c.Product).ToList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Problem in " + GetType().Name + " " +
                MethodBase.GetCurrentMethod()!.Name + " " + ex.Message);
                throw;
            }
            return fullCartProducts!;
        }
    }
}
