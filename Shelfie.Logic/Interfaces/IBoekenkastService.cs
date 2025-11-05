using System.Collections.Generic;

using Shelfie.Logic.DTOs;

namespace Shelfie.Logic.Interfaces;

public interface IBoekenkastService
{
    IReadOnlyList<BoekDto> GetBoekenkastForUser(int gebruikerId);
}
