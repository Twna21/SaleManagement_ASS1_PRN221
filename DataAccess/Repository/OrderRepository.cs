using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class OrderRepository : IOrderRepository
    {
        public void Add(Order order) => GenericDAO<Order>.Instance.AddNew(order);


        public void Delete(Order order) => GenericDAO<Order>.Instance.Remove(order);

        public IEnumerable<Order> List() => GenericDAO<Order>.Instance.GetList();
        public IEnumerable<Order> ListByMember(int id) => GenericDAO<Order>.Instance.GetListById(id, "MemberId");
      
        public void Update(Order order) => GenericDAO<Order>.Instance.Update(order);

    }
}
