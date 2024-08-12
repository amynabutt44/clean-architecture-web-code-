using domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace application
{
    public  class orderService
    {

        private readonly IOrder order;
        public  orderService( IOrder o) 
        {
           order = o;
        }

        

        public void Add(Orderr entity)
        {
            order.Add(entity);  
        }
        public void delete(Orderr entity)
        {
            order.delete(entity);
        }

        public IEnumerable<Orderr> GetAll()
        {

        return order.GetAll(); }



        public IEnumerable<Orderr> GetById(int c_id)
        {
            return order.GetById(c_id);
        }

        public List<Orderr> GetOrders(string userId)
        {

        return order.GetOrders(userId); }


    }
}
