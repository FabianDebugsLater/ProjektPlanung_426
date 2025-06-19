using Backend.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers;

[ApiController]
[Route("[controller]")]
public class HealthController : ControllerBase
{
    private readonly AppDbContext _context;

    public HealthController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("db")]
    public async Task<IActionResult> CheckDatabaseConnection()
    {
        try
        {
            // Versucht eine einfache Abfrage durchzuführen
            bool canConnect = await _context.Database.CanConnectAsync();
            
            if (canConnect)
                return Ok(new { Status = "Connected", Message = "Database connection successful!" });
            else
                return StatusCode(500, new { Status = "Error", Message = "Cannot connect to database" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Status = "Error", Message = ex.Message });
        }
    }
}