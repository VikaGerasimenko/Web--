using AutoDealersApi.Models;
using AutoDealersApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AutoDealersApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DealersController : ControllerBase
{
    private readonly IDealerRepository _repository;
    public DealersController(IDealerRepository repository) => _repository = repository;

    [HttpGet]
    public ActionResult<IEnumerable<Dealer>> GetAll() => Ok(_repository.GetAll());

    [HttpGet("{id}")]
    public ActionResult<Dealer> GetById(int id)
    {
        var dealer = _repository.GetById(id);
        return dealer is null ? NotFound() : Ok(dealer);
    }
}