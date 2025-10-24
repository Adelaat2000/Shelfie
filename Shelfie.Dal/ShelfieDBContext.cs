using Microsoft.EntityFrameworkCore;
using Shelfie.Dal.Models;

namespace Shelfie.Dal;

public class ShelfieDbContext : DbContext
{
	public DbSet<Gebruiker> Gebruikers { get; set; }
	//CONSTRUCTOR
	public ShelfieDbContext(DbContextOptions<ShelfieDbContext> options)
		: base(options)
	{
	}
}