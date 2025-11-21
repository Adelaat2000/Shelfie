namespace Shelfie.Contract.DTO;

    public record BoekDTO(
        int BoekID,
        string ISBN,
        string Titel,
        List<AuteurDTO> Auteurs
    );
