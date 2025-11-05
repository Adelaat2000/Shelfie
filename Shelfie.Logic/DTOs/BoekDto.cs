namespace Shelfie.Logic.DTOs;

public sealed class BoekDto
{
    public int BoekID { get; init; }
    public string? Titel { get; init; }
    public string? ISBN { get; init; }
    public string? AuteurNaam { get; init; }
}
