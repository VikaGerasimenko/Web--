using AutoDealersApi.Models;
using AutoDealersApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AutoDealersApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CarsController : ControllerBase
{
    private readonly ICarRepository _repository;
    public CarsController(ICarRepository repository) => _repository = repository;

    [HttpGet]
    public ActionResult<IEnumerable<Car>> GetAll() => Ok(_repository.GetAll());

    [HttpGet("{id}")]
    public ActionResult<Car> GetById(int id)
    {
        var car = _repository.GetById(id);
        return car is null ? NotFound() : Ok(car);
    }
}