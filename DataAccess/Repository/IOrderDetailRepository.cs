using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IOrderDetailRepository
    {

        public OrderDetail GetById(int id);
        void Delete(OrderDetail id);
        IEnumerable<OrderDetail> GetList();

        IEnumerable<OrderDetail> GetListByMember(int id);
        
    }
}
