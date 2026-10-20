using QuizEn.ViewModels;

namespace QuizEn.Views;

public partial class CardsPage : ContentPage
{
    public CardsPage()
    {
        InitializeComponent();

        
    }

    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}
