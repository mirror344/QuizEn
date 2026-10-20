
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using QuizEn.Models;
using QuizEn.Services;
using QuizEn.Views;

namespace QuizEn.ViewModels;

public class MainViewModel : INotifyPropertyChanged
{
    private readonly StudyService _studyService;

    public ObservableCollection<Deck> Decks { get; } = new();

    private int _learnedWords;
    public int LearnedWords
    {
        get => _learnedWords;
        private set => SetProperty(ref _learnedWords, value);
    }

    private int _studyStreak;
    public int StudyStreak
    {
        get => _studyStreak;
        private set => SetProperty(ref _studyStreak, value);
    }

    private int _completedCards;
    public int CompletedCards
    {
        get => _completedCards;
        private set => SetProperty(ref _completedCards, value);
    }

    private int _todayCompletedCards;
    public int TodayCompletedCards
    {
        get => _todayCompletedCards;
        private set => SetProperty(ref _todayCompletedCards, value);
    }

    public int DailyGoal => _studyService.DailyGoal;

    public double DailyGoalProgress =>
        DailyGoal <= 0
            ? 0
            : Math.Min(1.0, (double)TodayCompletedCards / DailyGoal);

    public string DailyGoalText =>
        $"{TodayCompletedCards} из {DailyGoal} карточек сегодня";

    public string DailyGoalPercent =>
        $"{DailyGoalProgress:P0}";

    public ICommand NavigateToFavoritesCommand { get; }
    public ICommand NavigateToCardsCommand { get; }

    public ICommand StudyDeckCommand { get; }

    public MainViewModel()
    {
        _studyService = new StudyService();

        NavigateToFavoritesCommand = new Command(
            async () => await Shell.Current.GoToAsync(
                nameof(FavoritesPage)
            ));
        NavigateToCardsCommand = new Command(
            async () => await Shell.Current.GoToAsync(
                nameof(CardsPage)    
            ));

        StudyDeckCommand = new Command<Deck>(
            async deck => await OpenDeck(deck));

        LoadData();
    }

    private void LoadData()
    {
        var activities = _studyService.GetActivities();

        LearnedWords = _studyService.GetLearnedWords(activities);
        StudyStreak = _studyService.GetStudyStreak(activities);
        CompletedCards = _studyService.GetCompletedCards(activities);
        TodayCompletedCards =
            _studyService.GetTodayCompletedCards(activities);

        OnPropertyChanged(nameof(DailyGoal));
        OnPropertyChanged(nameof(DailyGoalProgress));
        OnPropertyChanged(nameof(DailyGoalText));
        OnPropertyChanged(nameof(DailyGoalPercent));

        Decks.Clear();

        foreach (var deck in _studyService.GetDecks())
            Decks.Add(deck);
    }

    private async Task OpenDeck(Deck? deck)
    {
        if (deck == null)
            return;

        await Shell.Current.DisplayAlert(
            deck.Name,
            $"В колоде «{deck.Name}» {deck.CardsCount} карточек.\n" +
            "Экран изучения подключим следующим этапом.",
            "Понятно");
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private bool SetProperty<T>(
        ref T field,
        T value,
        [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value))
            return false;

        field = value;

        OnPropertyChanged(propertyName);
        OnPropertyChanged(nameof(DailyGoalProgress));
        OnPropertyChanged(nameof(DailyGoalText));
        OnPropertyChanged(nameof(DailyGoalPercent));

        return true;
    }

    private void OnPropertyChanged(
        [CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(
            this,
            new PropertyChangedEventArgs(propertyName));
    }
}
