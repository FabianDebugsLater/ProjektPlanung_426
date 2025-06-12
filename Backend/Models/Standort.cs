using System.ComponentModel.DataAnnotations;

namespace Backend.Models;

public class Standort
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    [Required]
    [MaxLength(255)]
    public string Adresse { get; set; }

    [Required]
    [MaxLength(10)]
    public string Postleitzahl { get; set; }

    [MaxLength(100)]
    public string? Ort { get; set; }

    public int? KundeId { get; set; }

    public DateTime ErstelltAm { get; set; } = DateTime.Now;

    public DateTime? AktualisiertAm { get; set; }
}