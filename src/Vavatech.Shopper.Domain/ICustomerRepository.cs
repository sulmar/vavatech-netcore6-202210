using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vavatech.Shopper.Domain
{

    public interface ICustomerRepository : IEntityRepository<Customer>
    {

    }

    //public class CustomerRepository : AbstractCustomerQueryRepository, AbstractCustomerCommandsRepository
    //{

    //}

    public class CustomerRepository : ICustomerQueryRepository, ICustomerCommandsRepository
    {
        public void Add(Customer customer)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
    }

    // CQRS

    // Query

    public abstract class AbstractCustomerQueryRepository
    {
        public abstract IEnumerable<Customer> Get();
        public abstract Customer Get(int id);       
    }

    // Query
    public interface ICustomerQueryRepository
    {
         IEnumerable<Customer> Get();
         Customer Get(int id);
    }

    // Commands
    public interface ICustomerCommandsRepository
    {
        void Add(Customer customer);
        void Update(Customer customer);
        void Remove(int id);
    }

    // Commands
    public abstract class AbstractCustomerCommandsRepository
    {
        public abstract void Add(Customer customer);
        public abstract void Update(Customer customer);
        public abstract void Remove(int id);
    }

   

}
