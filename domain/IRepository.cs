using System.Collections.Generic;
namespace domain.Models
{
    public interface IRepository<TEntity>
    {


        public void Add(TEntity entity);
        public void delete(TEntity entity);

        public IEnumerable<TEntity> GetAll();


        public IEnumerable<TEntity> GetById(int c_id);

    }
}
