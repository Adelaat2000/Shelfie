using Shelfie.Contract.Interfaces;
using Shelfie.Contract.OutputDTO;
using Shelfie.Logic.Mappers;

namespace Shelfie.Logic.Services;

public class BoekService
{
    private readonly IBoekRepository _repository;
    private readonly BoekMapper _boekMapper;
    
    public BoekService (IBoekRepository repository, BoekMapper boekMapper)
        {
        _repository = repository;
        _boekMapper = boekMapper;
        }

    public BoekOutputDTO GetDetails(string isbn)
    {
        var data = _repository.GetByIsbn(isbn);
        if (data==null) return null;

        var boek = _boekMapper.ToDomain(data);
        //boek.Auteurs.AddRange(_auteurRepository.GetAuteursForBoek(boek.BoekID));

        return _boekMapper.ToOutput(boek);

    }
}