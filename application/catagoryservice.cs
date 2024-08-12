using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using domain;
using domain.Models;



namespace application
{
    public class catagoryservice
    {
          
        private readonly  Icatagory _catagory;

         public catagoryservice (Icatagory c)
        {
            _catagory =c;
        }

        public int getid(string name)
        {
            return  _catagory.getid(name);
        }

        public void update(catagory c)
        {
             _catagory.update(c);
        }

        public void Add(catagory entity)
        {
            _catagory.Add(entity);
        }


        public void delete(catagory entity)
        {
            _catagory.delete(entity);
        }


        public IEnumerable<catagory> GetAll()
        {

        return _catagory.GetAll(); }
    



        public IEnumerable<catagory> GetById(int c_id)
        {
            return _catagory.GetById(c_id);
        }



    }
}
