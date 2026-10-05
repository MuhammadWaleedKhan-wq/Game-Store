namespace GameStore.Api.Dtos;

// A DTO is a contract between the client and server since
// represents a shared agreement about how data will be transferred and used.
public record GameDetailsDto(
    int Id,
    string Name,
    int Genre,
    decimal? Price,
    DateOnly ReleaseDate
);