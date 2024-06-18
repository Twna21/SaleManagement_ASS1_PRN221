using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IProductRepository
    {

        public IEnumerable<Product> ReadAll();
   
        public Product GetById(int id);
     
        public void Create(Product Product);
        public void Update(Product Product);
        public void Delete(Product Product);
        public void SetQuantity(int id, int quant);
    }
}
