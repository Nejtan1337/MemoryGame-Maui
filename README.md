Gra Memory (.NET MAUI)
Projekt gry logicznej typu Memory zrealizowany w języku C# przy użyciu frameworka .NET MAUI. Aplikacja polega na odnajdywaniu par identycznych kart na planszy.

Opis funkcjonalności
- Dynamiczny rozmiar planszy: Użytkownik definiuje rozmiar siatki (np. 4x4) przed rozpoczęciem rozgrywki.
- Mechanika gry: Losowe tasowanie kart przy każdym uruchomieniu, obsługa odkrywania kart i weryfikacja par.
- System punktacji: Zliczanie czasu gry oraz liczby wykonanych ruchów.
- Zapisywanie wyników: Najlepszy wynik dla danego rozmiaru planszy jest trwale zapisywany w pamięci urządzenia.
- Interfejs: Responsywny układ kart oparty na kontrolce CollectionView.

Technologie
- Język: C#
- Platforma: .NET 8.0 (MAUI)
- UI: XAML
- Architektura: Wykorzystanie mechanizmów Data Binding, ObservableCollection oraz interfejsu INotifyPropertyChanged do aktualizacji widoku
