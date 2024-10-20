using WorkBuddy.MAUI.Services;
using WorkBuddy.MAUI.ViewModels;

namespace WorkBuddy.MAUI.Views.Pages
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainPageViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}
