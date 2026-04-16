namespace Shared.Restaurant;

public record UpdateRestaurantDto
{
    public string? OpeningHours { get; init; }
    public string? ClosingHours { get; init; }
    public string? Type { get; init; }
    public decimal? Rating { get; init; } 
}