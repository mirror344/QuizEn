namespace QuizEn.Models;

public class Deck
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public int CardsCount { get; set; }

    public string Description { get; set; } = string.Empty;
}