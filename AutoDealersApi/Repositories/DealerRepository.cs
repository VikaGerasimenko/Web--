using AutoDealersApi.Data;
using AutoDealersApi.Models;

namespace AutoDealersApi.Repositories;

public class DealerRepository : IDealerRepository
{
    private readonly AppDbContext _context;

    public DealerRepository(AppDbContext context) => _context = context;

    public IEnumerable<Dealer> GetAll() => _context.Dealers.ToList();

    public Dealer? GetById(int id) => _context.Dealers.FirstOrDefault(d => d.Id == id);
}