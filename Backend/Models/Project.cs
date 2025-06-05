namespace Backend.Models;

public class Projekt
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int KundeId { get; set; }
    public int DauerGeschaetztStunden { get; set; }
    public int DauerBerechnetStunden { get; set; }
}