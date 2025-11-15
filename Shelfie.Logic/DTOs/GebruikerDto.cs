namespace Shelfie.Logic.DTOs
{
    public record GebruikerDto(
        int GebruikerId,
        string GebruikersNaam,
        string Email,
        string WachtwoordHash,
        string PersoonlijkeInfo,
        string BannerURL,
        string IcoonURL
    );
}