using AutoDealersApi.Data;
using AutoDealersApi.Models;

namespace AutoDealersApi.Repositories;

public class CarRepository : ICarRepository
{
    private readonly AppDbContext _context;

    public CarRepository(AppDbContext context) => _context = context;

    public IEnumerable<Car> GetAll() => _context.Cars.ToList();

    public Car? GetById(int id) => _context.Cars.FirstOrDefault(c => c.Id == id);
}