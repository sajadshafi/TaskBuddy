using System.Collections.ObjectModel;

namespace WorkBuddy.MAUI.Views.Pages;

public partial class CalendarPage : ContentPage
{

    public ObservableCollection<string> Items { get; set; } = [];
    private bool _isDropdownVisible = false;
    private string _selectedItemText = "";
    private string _selectedItem = "";

    public CalendarPage()
    {
        InitializeComponent();
    }

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        _isDropdownVisible = !_isDropdownVisible;

        DropdownOverlay.IsVisible = _isDropdownVisible;
    }
}