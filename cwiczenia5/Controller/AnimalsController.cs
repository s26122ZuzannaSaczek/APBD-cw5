using cwiczenia5.Models;
using cwiczenia5.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace cwiczenia5.Controller;


[ApiController]
//[Route("api/[controller]")]
[Route("api/animals")]
public class AnimalsController : ControllerBase
{
    private readonly IConfiguration _configuration;
    public AnimalsController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpGet]
    public IActionResult GetAnimals(string? orderBy)
    {
        if (orderBy is null)
            orderBy = "name";

        switch (orderBy)
        {
            case "name": break;
            case "description": break;
            case "category": break;
            case "area": break;
            default: orderBy = "name"; break;
        }
        
        
        List<Animal> animals = new List<Animal>();
        using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default")))
        {
            var command = connection.CreateCommand();
            command.CommandText = $"SELECT * FROM Animals order by {orderBy} ASC";
        
            connection.Open();
            
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                animals.Add(new Animal
                {
                    IdAnimal = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Description = reader.GetString(2),
                    Category = reader.GetString(3),
                    Area = reader.GetString(4)

                });
            }
        };
        
        return Ok(animals);
    }
    
    [HttpPost]
    public IActionResult AddAnimal(AddAnimal addAnimal)
    {
        // Otwieramy połączenie
        using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default")))
        {
            var command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "INSERT INTO Animals (Name, Description, Category, Area) VALUES (@1, @2 ,@3 , @4)";
            command.Parameters.AddWithValue("@1", addAnimal.Name);
            command.Parameters.AddWithValue("@2", addAnimal.Description);
            command.Parameters.AddWithValue("@3", addAnimal.Category);
            command.Parameters.AddWithValue("@4", addAnimal.Area);
            connection.Open();
            // Wykonanie zapytania
            command.ExecuteNonQuery();
        }
        
        return Created("", null);
    }

    [HttpPut("{idAnimal:int}")]
    public IActionResult EditAnimal(int idAnimal, AddAnimal animal)
    {
        // Otwieramy połączenie
        using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default")))
        {
            var command = connection.CreateCommand();
            command.CommandText = "Select IdAnimal from Animals where IdAnimal=@5";
                        command.Parameters.AddWithValue("@5", idAnimal);
                        connection.Open();
            
                        if (command.ExecuteScalar() is null)
                        {
                            return NotFound($"Animal with id {idAnimal} was not found");
                        }
                        
            command.CommandText = "UPDATE Animals SET Name = @1, Description = @2, Category = @3, Area = @4 WHERE IdAnimal=@6 ";
            command.Parameters.AddWithValue("@1", animal.Name);
            command.Parameters.AddWithValue("@2", animal.Description);
            command.Parameters.AddWithValue("@3", animal.Category);
            command.Parameters.AddWithValue("@4", animal.Area);
            command.Parameters.AddWithValue("@6", idAnimal);
            // Wykonanie zapytania
            command.ExecuteNonQuery();
        }
        return Ok();
        
    }
    
    [HttpDelete("{idAnimal:int}")]
    public IActionResult DeleteAnimal(int id)
    {
        // Otwieramy połączenie
        using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default")))
        {
            var command = connection.CreateCommand();
            command.CommandText = "Select IdAnimal from Animals where IdAnimal=@1";
            command.Parameters.AddWithValue("@1", id);
            connection.Open();
            
            if (command.ExecuteScalar() is null)
            {
                return NotFound($"Animal with id {id} was not found");
            }
                        
            command.CommandText = "DELETE FROM Animals WHERE IdAnimal=@1 ";
            // Wykonanie zapytania
            command.ExecuteNonQuery();
        }
        return Ok();
        
    }
    
    
}