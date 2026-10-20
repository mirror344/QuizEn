namespace QuizEn.Models;

public class FavoriteCard
{
    public int Id { get; set; }
    public string Word { get; set; } = string.Empty;
    public string Translation { get; set; } = string.Empty;
    public string Example { get; set; } = string.Empty;
    public string DeckName { get; set; } = string.Empty;
}