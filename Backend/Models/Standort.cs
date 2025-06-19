using System.ComponentModel.DataAnnotations;

namespace Backend.Models;

public class Standort
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public required string Name { get; set; }

    [Required]
    [MaxLength(255)]
    public required string Adresse { get; set; }

    [Required]
    [MaxLength(10)]
    public required string Postleitzahl { get; set; }

    [MaxLength(100)]
    public string? Ort { get; set; }

    public int? KundeId { get; set; }

    public DateTime ErstelltAm { get; set; } = DateTime.Now;

    public DateTime? AktualisiertAm { get; set; }
}