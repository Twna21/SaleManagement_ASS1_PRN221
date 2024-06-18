using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class ProductRepository : IProductRepository
    {
        public void Create(Product Product) => GenericDAO<Product>.Instance.AddNew(Product);

        public void Delete(Product Product) => GenericDAO<Product>.Instance.Remove(Product);

        public Product GetById(int id) => GenericDAO<Product>.Instance.GetById(id);

        public void SetQuantity(int id, int quant) => GenericDAO<Product>.Instance.UpdateQuantityInDB(id,quant);

        public IEnumerable<Product> ReadAll() => GenericDAO<Product>.Instance.GetList();
     
        public void Update(Product Product) => GenericDAO<Product>.Instance.Update(Product);

     
    }
}
