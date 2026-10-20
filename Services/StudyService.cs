
using QuizEn.Models;

namespace QuizEn.Services;

public class StudyService
{
    public int DailyGoal { get; } = 20;

    public List<Deck> GetDecks()
    {
        return new List<Deck>
        {
            new()
            {
                Id = 1,
                Name = "Business English",
                CardsCount = 120,
                Description = "Английский для работы и бизнеса"
            },
            new()
            {
                Id = 2,
                Name = "Travel Vocabulary",
                CardsCount = 85,
                Description = "Слова для путешествий"
            },
            new()
            {
                Id = 3,
                Name = "IT English",
                CardsCount = 150,
                Description = "Техническая лексика"
            },
            new()
            {
                Id = 4,
                Name = "Daily Conversation",
                CardsCount = 96,
                Description = "Повседневное общение"
            }
        };
    }

    public List<StudyActivity> GetActivities()
    {
        
        return new List<StudyActivity>
        {
            new()
            {
                Date = DateTime.Today.AddDays(-3),
                CompletedCards = 12,
                LearnedWords = 8
            },
            new()
            {
                Date = DateTime.Today.AddDays(-2),
                CompletedCards = 18,
                LearnedWords = 10
            },
            new()
            {
                Date = DateTime.Today.AddDays(-1),
                CompletedCards = 15,
                LearnedWords = 6
            },
            new()
            {
                Date = DateTime.Today,
                CompletedCards = 8,
                LearnedWords = 5
            }
        };
    }

    public int GetLearnedWords(List<StudyActivity> activities)
    {
        return activities.Sum(a => a.LearnedWords);
    }

    public int GetCompletedCards(List<StudyActivity> activities)
    {
        return activities.Sum(a => a.CompletedCards);
    }

    public int GetTodayCompletedCards(List<StudyActivity> activities)
    {
        return activities
            .Where(a => a.Date.Date == DateTime.Today)
            .Sum(a => a.CompletedCards);
    }

    public int GetStudyStreak(List<StudyActivity> activities)
    {
        var studyDates = activities
            .Where(a => a.CompletedCards > 0 || a.LearnedWords > 0)
            .Select(a => a.Date.Date)
            .Distinct()
            .ToHashSet();

        DateTime day = DateTime.Today;

        if (!studyDates.Contains(day))
            day = day.AddDays(-1);

        int streak = 0;

        while (studyDates.Contains(day))
        {
            streak++;
            day = day.AddDays(-1);
        }

        return streak;
    }
}
