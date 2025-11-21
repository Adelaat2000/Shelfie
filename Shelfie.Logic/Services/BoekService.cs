using Shelfie.Contract.Interfaces;
using Shelfie.Logic.Mappers;
using Shelfie.Logic.Models;

namespace Shelfie.Logic.Services;
    public class BoekService
    {
        private readonly IBoekRepository _boekRepository;
        private readonly BoekMapper _boekMapper;

        public BoekService(IBoekRepository boekRepository, BoekMapper boekMapper)
        {
            _boekRepository = boekRepository;
            _boekMapper = boekMapper;
        }

        public Boek? GetByIsbn(string isbn)
        {
            var dto = _boekRepository.GetByIsbn(isbn);
            return dto == null ? null : _boekMapper.ToDomain(dto);
        }

        public IEnumerable<Boek> GetByTitel(string titel)
        {
            var dtos = _boekRepository.GetByTitel(titel);
            return dtos.Select(_boekMapper.ToDomain);
        }

        public IEnumerable<Boek> GetByAuteur(int auteurId)
        {
            var dtos = _boekRepository.GetByAuteurID(auteurId);
            return dtos.Select(_boekMapper.ToDomain);
        }

        public Boek AddBoek(Boek boek, List<int> auteurIds)
        {
            var dto = _boekMapper.ToDto(boek);
            var createdDto = _boekRepository.Insert(dto, auteurIds);
            var stored = _boekRepository.GetByIsbn(createdDto.ISBN) ?? createdDto;
            return _boekMapper.ToDomain(stored);
        }

        public Boek UpdateBoek(int boekId, Boek boek, List<int> auteurIds)
        {
            var dto = _boekMapper.ToDto(boek, boekId);
            var updatedDto = _boekRepository.Update(dto, auteurIds);
            var stored = _boekRepository.GetByIsbn(updatedDto.ISBN) ?? updatedDto;
            return _boekMapper.ToDomain(stored);
        }
    }