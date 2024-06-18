using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        public OrderDetail GetById(int id) => GenericDAO<OrderDetail>.Instance.GetByOrderId(id);
        public void Delete(OrderDetail id) => GenericDAO<OrderDetail>.Instance.Remove(id);

        public IEnumerable<OrderDetail> GetList() => GenericDAO<OrderDetail>.Instance.GetList();
        public IEnumerable<OrderDetail> GetListByMember(int id) => GenericDAO<OrderDetail>.Instance.GetOrderDetailsByMemberId(id);



    }
}
