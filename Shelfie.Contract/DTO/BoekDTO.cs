namespace Shelfie.Contract.DTO;

    public record BoekDTO(
        int BoekId,
        string ISBN,
        string Titel,
        List<AuteurDTO> Auteurs,
        List<GenreDTO> Genres
    );
