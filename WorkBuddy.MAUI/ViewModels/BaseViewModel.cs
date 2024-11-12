using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WorkBuddy.MAUI.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string? name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        bool isBusy;

        public Command NavigateToCommand { get; }

        public BaseViewModel()
        {
            NavigateToCommand = new Command(async (object path) => await NavigateAsync((string)path));
        }

        public bool IsBusy
        {
            get => isBusy;
            set
            {
                if (isBusy == value)
                    return;
                isBusy = value;
                OnPropertyChanged();
            }
        }

        private async Task NavigateAsync(string path)
        {
            ArgumentException.ThrowIfNullOrEmpty(nameof(path));

            var currentPage = Shell.Current.CurrentPage.GetType().Name;

            if(path ==  currentPage)
            {
                return;
            }

            await Shell.Current.GoToAsync(path);
        }

        public bool IsNotBusy => !isBusy;
    }
}
