namespace Shelfie.Contract.DTO
{
    public record GebruikerDTO(
        int GebruikerId,
        string GebruikersNaam,
        string Email,
        string WachtwoordHash,
        string? PersoonlijkeInfo,
        string? BannerURL,
        string? IcoonURL
    );
}