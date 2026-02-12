namespace Gra_Memory
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnStartGameClicked(object sender, EventArgs e)
        {
            string input = SizeEntry.Text;

            if (int.TryParse(input, out int size))
            {
                if (size % 2 != 0)
                {
                    await DisplayAlert("Błąd", "Rozmiar planszy musi być liczbą parzystą (np. 4, 6, 8), żeby pary się zgadzały!", "OK");
                    return;
                }

                if (size < 2 || size > 8)
                {
                    await DisplayAlert("Błąd", "Wpisz liczbę od 2 do 8. Za duża plansza.", "OK");
                    return;
                }

                await Shell.Current.GoToAsync($"{nameof(GamePage)}?Size={size}");
            }
            else
            {
                await DisplayAlert("Błąd", "Proszę wpisać poprawną liczbę.", "OK");
            }
        }
    }
}