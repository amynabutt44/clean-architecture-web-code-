namespace domain.Models
{
    public interface Iproduct :IRepository<product>
    {

        public IEnumerable<product> GetProductsByName(string name);
        public void update_price(product p);

        public void Add(product entity);
        public void delete(product entity);

        public IEnumerable<product> GetAll();


        public IEnumerable<product> GetById(int c_id);


    }
}
