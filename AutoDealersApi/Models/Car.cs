namespace AutoDealersApi.Models;

public class Car
{
    public int Id { get; set; }
    public string Firm { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public int Year { get; set; }
    public int Power { get; set; }
    public string? Color { get; set; }
    public int Price { get; set; }
    public int DealerId { get; set; }
}