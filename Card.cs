using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Gra_Memory
{
    public class Card : INotifyPropertyChanged
    {
        public string Content
        { 
            get;
            set;
        }

        private bool _isVisible;
        public bool IsVisible
        {
            get => _isVisible;
            set 
            { 
                _isVisible = value; 
                OnPropertyChanged(); 
                OnPropertyChanged(nameof(DisplayContent)); 
            }
        }

        private bool _isMatched;
        public bool IsMatched
        {
            get => _isMatched;
            set 
            { 
                _isMatched = value; 
                OnPropertyChanged();
                OnPropertyChanged(nameof(DisplayContent));
            }
        }

        public string DisplayContent => (IsVisible || IsMatched) ? Content : "❓";

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}