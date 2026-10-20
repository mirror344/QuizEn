using QuizEn.Views;
namespace QuizEn;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(nameof(FavoritesPage), typeof(FavoritesPage));
		Routing.RegisterRoute(nameof(CardsPage), typeof(CardsPage));
	}
}
