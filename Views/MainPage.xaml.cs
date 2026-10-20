using Microsoft.Maui.Controls;
using QuizEn.Views;

namespace QuizEn.Views;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}

	private async void OnFavoritesClicked(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync(nameof(FavoritesPage));
	}


}
