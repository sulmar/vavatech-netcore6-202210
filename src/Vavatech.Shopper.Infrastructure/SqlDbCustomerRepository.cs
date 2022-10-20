using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vavatech.Shopper.Domain;

namespace Vavatech.Shopper.Infrastructure
{
    internal class SqlDbCustomerRepository : ICustomerRepository
    {
        private readonly SqlConnection connection;

        public SqlDbCustomerRepository(SqlConnection connection)
        {
            this.connection = connection;
        }

        public void Add(Customer customer)
        {
            /* SQL
              CREATE OR ALTER PROCEDURE dbo.uspAddCustomer(@FirstName nvarchar(50), @LastName nvarchar(50), out @CustomerId int)
              (  

              )
            */

            string sql = "dbo.uspAddCustomer";

            // var command = connection.CreateCommand();
            var command = new SqlCommand(sql, connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@FirstName", customer.FirstName);
            command.Parameters.AddWithValue("@LastName", customer.LastName);

            command.Parameters["CustomerId"].Direction = System.Data.ParameterDirection.ReturnValue;            

            var id = (int)command.Parameters["CustomerId"].Value;
            customer.Id = id;
        }

        public IEnumerable<Customer> Get()
        {
            throw new NotImplementedException();
        }

        public Customer Get(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();

        }

        public void Update(Customer customer)
        {
            string sql = "UPDATE Customers SET FirstName=@FirstName, Salary=@Salary";

            throw new NotImplementedException();
        }
    }
}
