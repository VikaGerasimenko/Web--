using AutoDealersApi.Models;

namespace AutoDealersApi.Repositories;

public interface ICarRepository
{
    IEnumerable<Car> GetAll();
    Car? GetById(int id);
}