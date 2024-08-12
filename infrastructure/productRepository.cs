using Dapper;
using Microsoft.Data.SqlClient;
using domain;
using domain.Models;

namespace infrastructure.Models
{
    public class productRepository : Iproduct

    {

        private readonly IRepository<product> _repository;

        public  productRepository(IRepository<product> repository)
        {
            _repository = repository;
        }

        public void Add(product entity)
        {
            _repository.Add(entity);
        }
        public void delete(product entity)
        {
            _repository.delete(entity);

        }

        public IEnumerable<product> GetAll()
        {
            return  _repository.GetAll();
        }


        public IEnumerable<product> GetById(int c_id)
        {
            return _repository.GetById(c_id);
        }


        string connectionstring = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=mydb2;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
        public void update_price(product p)
        {

            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                string query = "UPDATE product SET name = @n, price = @p WHERE Id = @i";

                connection.Open();
                try
                {
                    connection.Execute(query, new { n = p.name, i = p.Id, p = p.price });
                }
                catch (Exception ex)
                {
                    // Handle any exceptions, e.g., logging
                    Console.WriteLine(ex.Message);
                }
            }
        }
        public IEnumerable<product> GetProductsByName(string name)
        {
            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                string query = "SELECT * FROM product WHERE name LIKE @name";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@name", "%" + name + "%");

                connection.Open();
                try
                {
                    return connection.Query<product>(query, parameters);
                }
                catch (Exception ex)
                {
                    // Handle any exceptions, e.g., logging
                    Console.WriteLine(ex.Message);
                    return Enumerable.Empty<product>();
                }
            }
        }


    }
}
