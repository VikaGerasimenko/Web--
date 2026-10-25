using AutoDealersApi.Models;

namespace AutoDealersApi.Repositories;

public interface IDealerRepository
{
    IEnumerable<Dealer> GetAll();
    Dealer? GetById(int id);
}