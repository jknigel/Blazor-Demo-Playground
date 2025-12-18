namespace WeatherAppz.Models;
public class User
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required string Username { get; set; }
    public required string Email { get; set; }
    public required Address Address { get; set; }
}

public class Address
{
    public required string Street { get; set; }
    public required string Suite { get; set; }
    public required string City { get; set; }
    public required string Zipcode { get; set; }
    public required Geo Geo { get; set; }
}

public class Geo
{
    public required string Lat { get; set; }
    public required string Lng { get; set; }
}