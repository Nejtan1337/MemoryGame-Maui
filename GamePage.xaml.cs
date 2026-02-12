using System.Collections.ObjectModel;

namespace Gra_Memory
{
    [QueryProperty(nameof(GridSize), "Size")]
    public partial class GamePage : ContentPage
    {
        public ObservableCollection<Card> Cards 
        {
            get;
            set;
        } = new();

        private IDispatcherTimer _timer;
        private int _secondsElapsed;
        private int _moves;
        private string _timerText;
        public string TimerText
        {
            get => _timerText;
            set { _timerText = value; OnPropertyChanged(); }
        }

        private string _movesText;
        public string MovesText
        {
            get => _movesText;
            set { _movesText = value; OnPropertyChanged(); }
        }

        private int _gridSpan;
        public int GridSpan
        {
            get => _gridSpan;
            set { _gridSpan = value; OnPropertyChanged(); }
        }

        private int _gridSize;
        public int GridSize
        {
            get => _gridSize;
            set
            {
                _gridSize = value;
                GridSpan = value;
                InitializeGame();
            }
        }

        private Card firstCardSelected;
        private bool isProcessing = false;

        public GamePage()
        {
            InitializeComponent();
            BindingContext = this;

            _timer = Application.Current.Dispatcher.CreateTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += (s, e) =>
            {
                _secondsElapsed++;
                TimeSpan time = TimeSpan.FromSeconds(_secondsElapsed);
                TimerText = $"Czas: {time:mm\\:ss}";
            };
        }

        private void InitializeGame()
        {
            Cards.Clear();
            _secondsElapsed = 0;
            _moves = 0;
            TimerText = "Czas: 00:00";
            MovesText = "Ruchy: 0";

            _timer.Start();

            List<string> emojis = new List<string>
            {
                "🐶", "🐱", "🐭", "🐹", "🐰", "🦊", "🐻", "🐼",
                "🐨", "🐯", "🦁", "🐮", "🐷", "🐸", "🐵", "🐔",
                "🐧", "🐦", "🦄", "🐝", "🐞", "🦋", "🐌", "🐙",
                "🦑", "🦐", "🦞", "🦀", "🐡", "🐠", "🐟", "🐬"
            };

            int totalPairs = (_gridSize * _gridSize) / 2;

            if (totalPairs > emojis.Count) totalPairs = emojis.Count;

            var gameIcons = emojis.Take(totalPairs).ToList();
            gameIcons.AddRange(gameIcons);

            var shuffledIcons = gameIcons.OrderBy(x => Guid.NewGuid()).ToList();

            foreach (var icon in shuffledIcons)
            {
                Cards.Add(new Card { Content = icon });
            }
            CardsCollection.ItemsSource = Cards;
        }

        private async void OnCardTapped(object sender, TappedEventArgs e)
        {
            if (isProcessing) return;
            var selectedCard = e.Parameter as Card;
            if (selectedCard == null || selectedCard.IsVisible || selectedCard.IsMatched) return;

            selectedCard.IsVisible = true;

            if (firstCardSelected == null)
            {
                firstCardSelected = selectedCard;
            }
            else
            {
                _moves++;
                MovesText = $"Ruchy: {_moves}";

                isProcessing = true;

                if (firstCardSelected.Content == selectedCard.Content)
                {
                    firstCardSelected.IsMatched = true;
                    selectedCard.IsMatched = true;
                    firstCardSelected = null;
                    isProcessing = false;
                    CheckWinCondition();
                }
                else
                {
                    await Task.Delay(1000);
                    firstCardSelected.IsVisible = false;
                    selectedCard.IsVisible = false;
                    firstCardSelected = null;
                    isProcessing = false;
                }
            }
        }

        private async void CheckWinCondition()
        {
            if (Cards.All(c => c.IsMatched))
            {
                _timer.Stop();

                string scoreKey = $"HighScore_{_gridSize}";

                int currentBest = Preferences.Get(scoreKey, 10000);

                string message = $"Twój wynik: {_moves} ruchów w czasie {TimerText.Replace("Czas: ", "")}.";

                if (_moves < currentBest)
                {
                    Preferences.Set(scoreKey, _moves);
                    message += $"\nNOWY REKORD! (Poprzedni: {(currentBest == 10000 ? "brak" : currentBest.ToString())})";
                }
                else
                {
                    message += $"\nNajlepszy wynik dla planszy {_gridSize}x{_gridSize}: {currentBest} ruchy";
                }

                await DisplayAlert("Gratulacje!", message, "OK");
                await Shell.Current.GoToAsync("..");
            }
        }

        private async void OnBackClicked(object sender, EventArgs e)
        {
            _timer.Stop();
            await Shell.Current.GoToAsync("..");
        }
    }
}