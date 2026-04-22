namespace Domain.Entities;

public class Restaurant : BaseEntity<int>
{
    public string OpeningHours { get; set; } = string.Empty;
    public string ClosingHours { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public decimal Rating { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string Name {  get; set; } = string.Empty;

    //public User User { get; set; } = null!;
    public ICollection<FoodItem> FoodItems { get; set; } = new List<FoodItem>();
}