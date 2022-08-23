using CustomerRepository.Entities;
using CustomerRepository.Interfaces;

namespace CustomerRepository.Repositories;

public class AddressRepository:BaseRepository,IRepository<Address>
{
    public Address Create(Address entity)
    {
        throw new NotImplementedException();
    }

    public Address Read(int entityId)
    {
        throw new NotImplementedException();
    }

    public void Update(Address entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(int entityId)
    {
        throw new NotImplementedException();
    }
}