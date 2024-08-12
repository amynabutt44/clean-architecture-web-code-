using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using domain;
using domain.Models;

namespace infrastructure.Models
{
    public class CatagoryRepositery: Icatagory
    {
        private readonly string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=mydb2;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

        private readonly IRepository<catagory> repository;
        public CatagoryRepositery(  IRepository<catagory> repository )
        {
            this.repository = repository;
        }


        public void Add(catagory entity)
        {
            repository.Add(entity);
        }
        public void delete(catagory entity)
        {
            repository.delete(entity);
        }


        public IEnumerable<catagory> GetAll()
        {
            return repository.GetAll();
        }


        public IEnumerable<catagory> GetById(int c_id)
        {
            return repository.GetById(c_id);
        }
        public int getid(string name)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT ID FROM catagory WHERE name = @name";
                return connection.QuerySingleOrDefault<int>(query, new { name });
            }
        }

        public void update(catagory c)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE catagory SET image = @image WHERE Id = @Id";
                connection.Execute(query, new { c.image, c.Id });
            }
        }
    }

    
}
