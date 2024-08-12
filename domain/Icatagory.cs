namespace domain.Models
{
    public interface Icatagory :IRepository<catagory>
    {

        public int getid(string name);
        public void update(catagory c);
        public void Add(catagory entity);
        public void delete(catagory entity);

        public IEnumerable<catagory> GetAll();


        public IEnumerable<catagory> GetById(int c_id);

    }
}
