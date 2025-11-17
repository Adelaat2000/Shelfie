namespace Shelfie.Contract.DTO
{
    public record RegisterDTO(
        int GebruikerId,
        string GebruikersNaam,
        string Email,
        string Wachtwoord
    );
}