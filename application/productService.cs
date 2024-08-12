using domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using domain;
using domain.Models;

namespace application
{
    public class productService
    {

        private readonly Iproduct p;
         public productService( Iproduct pr) {
            this.p = pr;
        }
        public IEnumerable<product> GetProductsByName(string name)
        {
            return p.GetProductsByName(name);
        }
        public void update_price(product pp)
        {
            p.update_price(pp);
        }


        public void Add(product entity)
        {
            p.Add(entity);
        }


        public void delete(product entity)
        {
            p.delete(entity);
        }

        public IEnumerable<product> GetAll()
        {

        return p.GetAll();
        }
    


        public IEnumerable<product> GetById(int c_id)
        {
          return  p.GetById(c_id);
         }
    }
}
