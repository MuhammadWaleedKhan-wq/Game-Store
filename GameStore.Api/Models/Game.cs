namespace GameStore.Api.Models;

public class Game
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public Genre? Genre { get; set; } // composite property
    public int GenreId { get; set; } // foreign key property
    public decimal? Price { get; set; }
    public DateOnly ReleaseDate { get; set; }
    public Game() { }
    public Game(int id, string name, Genre genre, decimal price, DateOnly releaseDate)
    {
        Id = id;
        Name = name;
        Genre = genre;
        Price = price;
        ReleaseDate = releaseDate;
    }
}