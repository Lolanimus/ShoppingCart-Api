using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Store.Infrastracture.Repository;
using Store.UseCases.Interfaces.Repository;
using Store.Models;
using System.Diagnostics;
using System.Reflection;

namespace Store.Infrastracture.DAL
{
    public class ProductDAO
    {
        private readonly IRepository<Product> repo;

        public ProductDAO()
        {
            repo = new StoreRepository<Product>();
        }

        public async Task<List<Product>> GetAll()
        {
            return await repo.GetAll();
        }

        public async Task<Product> GetById(int id)
        {
            Product? selectedProduct;
            try
            {
                StoreContext _db = new();
                selectedProduct = await repo.GetOne(product => product.Id == id);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Problem in " + GetType().Name + " " +
                MethodBase.GetCurrentMethod()!.Name + " " + ex.Message);
                throw;
            }
            return selectedProduct!;
        }
    }
}
