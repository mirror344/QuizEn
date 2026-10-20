using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using QuizEn.Models;

namespace QuizEn.ViewModels;

public class FavoritesViewModel : INotifyPropertyChanged
{
    private readonly List<FavoriteCard> _allFavorites = new();
    private string _searchText = string.Empty;
    public ObservableCollection<FavoriteCard> Favorites { get; } = new();

    public string SearchText
    {
        get => _searchText;
        set
        {
            if (_searchText == value) return;

            _searchText = value;
            OnPropertyChanged();

            FilterFavorites();
        }
    }

    public int FavoritesCount => Favorites.Count;

    public ICommand RemoveFavoriteCommand { get; }

    public FavoritesViewModel()
    {
        RemoveFavoriteCommand = new Command<FavoriteCard>(RemoveFavorite);
        LoadFavorites();
    }

    private void LoadFavorites()
    {
        // Временные данные для проверки интерфейса.
        _allFavorites.Add(new FavoriteCard
        {
            Id = 1,
            Word = "Achieve",
            Translation = "Достигать",
            Example = "I want to achieve my goals.",
            DeckName = "Основные слова"
        });

        _allFavorites.Add(new FavoriteCard
        {
            Id = 2,
            Word = "Improve",
            Translation = "Улучшать",
            Example = "I want to improve my English.",
            DeckName = "Глаголы"
        });

        _allFavorites.Add(new FavoriteCard
        {
            Id = 3,
            Word = "Knowledge",
            Translation = "Знания",
            Example = "Knowledge is important.",
            DeckName = "Учёба"
        });

        FilterFavorites();
    }

    private void FilterFavorites()
    {
        Favorites.Clear();

        string query = SearchText.Trim();

        foreach (FavoriteCard favorite in _allFavorites)
        {
            bool matches =
                string.IsNullOrWhiteSpace(query) ||
                favorite.Word.Contains(
                    query,
                    StringComparison.OrdinalIgnoreCase) ||
                favorite.Translation.Contains(
                    query,
                    StringComparison.OrdinalIgnoreCase);

            if (matches)
                Favorites.Add(favorite);
        }

        OnPropertyChanged(nameof(FavoritesCount));
    }

    private void RemoveFavorite(FavoriteCard? favorite)
    {
        if (favorite == null)
            return;

        _allFavorites.Remove(favorite);

        FilterFavorites();
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged(
        [CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(
            this,
            new PropertyChangedEventArgs(propertyName));
    }
}
