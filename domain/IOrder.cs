namespace domain.Models
{
    public interface IOrder :IRepository<Orderr>
    {
       

        public List<Orderr> GetOrders(string userId);
        public void Add(Orderr entity);
        public void delete(Orderr entity);

        public IEnumerable<Orderr> GetAll();


        public IEnumerable<Orderr> GetById(int c_id);
    }
}
