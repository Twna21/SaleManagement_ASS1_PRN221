using BusinessObject;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SaleLibrary.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess
{
    public class GenericDAO<T> where T : class
    {
        private static GenericDAO<T> instance = null;
        private static readonly object instanceLock = new object();
        private GenericDAO() { }

        public static GenericDAO<T> Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new GenericDAO<T>();
                    }
                    return instance;
                }
            }
        }

        public IEnumerable<T> GetList()
        {
            List<T> list;
            try
            {
                var _db = new FstoreContext();
                list = _db.Set<T>().ToList();
            }
            catch (Exception ex)
            {
                Exception exception = new Exception(ex.Message);
                throw exception;
            }
            return list;
        }

        public T GetById(int id)
        {
            T entity = null;
            try
            {
                var _db = new FstoreContext();
                entity = _db.Set<T>().Find(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return entity;
        }

        public IEnumerable<T> GetListById(int id,string propertyName)
        {
            List<T> list;
            try
            {
                var _db = new FstoreContext();
                list = _db.Set<T>().Where(entity => EF.Property<int>(entity, propertyName) == id).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }

        public OrderDetail GetByOrderId(int orderId)
        {
            OrderDetail entity = null;
            try
            {
                using (var _db = new FstoreContext())
                {
                    entity = _db.OrderDetails.SingleOrDefault(od => od.OrderId == orderId);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return entity;
        }

        public T GetByString(string propertyName, string value)
        {
            T entity = null;
            try
            {
                var _db = new FstoreContext();
                entity = _db.Set<T>().SingleOrDefault(e => EF.Property<string>(e, propertyName) == value);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return entity;
        }

        public void AddNew(T entity)
        {
            try
            {
                var _db = new FstoreContext();
                _db.Set<T>().Add(entity);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Remove(T entity)
        {
            try
            {
                var _db = new FstoreContext();
                _db.Set<T>().Remove(entity);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(T entity)
        {
            try
            {
                var _db = new FstoreContext();
                _db.Entry(entity).State = EntityState.Modified;
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateQuantityInDB(int productId, int newQuantity)
        {
            using (var context = new FstoreContext())
            {
                var product = context.Products.Find(productId);
                if (product != null)
                {
                    product.UnitsInStock = newQuantity;
                    context.SaveChanges();
                }
            }
        }

        public IEnumerable<OrderDetail> GetOrderDetailsByMemberId(int memberId)
        {
            List<OrderDetail> orderDetails;
            try
            {
                using (var _db = new FstoreContext())
                {
                  
                    var orders = _db.Orders.Where(o => o.MemberId == memberId).Select(o => o.OrderId).ToList();

                 
                    orderDetails = _db.OrderDetails.Where(od => orders.Contains(od.OrderId)).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return orderDetails;
        }


    }
}
