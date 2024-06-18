using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IOrderRepository
    {
        void Add(Order order);
        void Delete(Order order);
        void Update(Order order);
        IEnumerable<Order> List(); 
        IEnumerable<Order> ListByMember(int id);
    }
}
