using Shelfie.Contract.Interfaces;
using Shelfie.Logic.Interfaces;
using Shelfie.Logic.Models;

namespace Shelfie.Logic.Services;

public class BoekenkastService : IBoekenkastService
{
    private readonly IBoekRepository _boekRepo;

    public BoekenkastService(IBoekRepository boekRepo)
    {
        _boekRepo = boekRepo;
    }

}
