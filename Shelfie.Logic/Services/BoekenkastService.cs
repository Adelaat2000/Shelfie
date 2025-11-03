using Shelfie.Logic.Interfaces;
using Shelfie.Logic.Models;
using System;
using System.Collections.Generic;

public class BoekenkastService : IBoekenkastService
{
    private readonly IBoekRepository _boekRepo;
    public BoekenkastService(IBoekRepository boekRepo)
    {
        _boekRepo = boekRepo;
    }

    public List<Boek> GetBoekenkastForUser(int gebruikerId)
    {
        if (gebruikerId <= 0)
        {
            return new List<Boek>();
        }
        var boekenLijst = _boekRepo.GetBoekenVoorGebruiker(gebruikerId);
        return boekenLijst;
    }
}