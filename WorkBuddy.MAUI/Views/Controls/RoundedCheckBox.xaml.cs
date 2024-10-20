#if ANDROID
using CommunityToolkit.Maui.Extensions;
#endif

namespace WorkBuddy.MAUI.Views.Controls;

public partial class RoundedCheckBox : ContentView
{
    private Color _activeColor = Color.FromArgb("#fa5b14");

    private int _height = 16;
    private int _width = 16;

    public static readonly BindableProperty IsCheckedProperty =
  BindableProperty.Create(nameof(IsChecked), typeof(bool), typeof(CheckBox), false);

    public RoundedCheckBox()
    {
        InitializeComponent();
        CustomCheckboxBorder.WidthRequest = _height;
        CustomCheckboxBorder.HeightRequest = _width;
        checkBoxTick.WidthRequest = _height - 5;
        checkBoxTick.HeightRequest = _width - 5;
    }

    private void OnChecked(object sender, TappedEventArgs e)
    {
        IsChecked = !IsChecked;

        if (IsChecked)
        {
#if ANDROID
            checkBoxTick.FadeTo(1, 150, Easing.Linear);
            CustomCheckboxBorder.BackgroundColorTo(ActiveColor);
#endif
#if IOS
                checkBoxTick.FadeTo(0, 150, Easing.Linear);
#endif
            return;
        }

#if ANDROID
        checkBoxTick.FadeTo(0, 150, Easing.Linear);
        CustomCheckboxBorder.BackgroundColorTo(Color.FromRgba("#00000000"));
#endif

#if IOS
            checkBoxTick.FadeTo(1, 150, Easing.Linear);
#endif
    }

    // Optional: You can expose a public property to get or set the checked state
    public bool IsChecked
    {
        get => (bool)GetValue(IsCheckedProperty);
        //get => _isChecked;
        set
        {
            SetValue(IsCheckedProperty, value);
            //if (IsChecked == value)
            //    return;

            //_isChecked = value;
            checkBoxTick.IsVisible = value;
            CustomCheckboxBorder.BackgroundColor = ActiveColor;
        }
    }

    public int ElementHeight
    {
        get => _height;
        set
        {
            if (_height != value)
            {
                _height = value;
                ElementWidth = value;
                CustomCheckboxBorder.HeightRequest = _height;
            }
        }
    }

    public int ElementWidth
    {
        get => _width;
        set
        {
            if (_width != value)
            {
                _width = value;
                ElementHeight = value;
                CustomCheckboxBorder.WidthRequest = _width;
            }
        }
    }

    public Color ActiveColor
    {
        get => _activeColor;
        set
        {
            _activeColor = value;
        }
    }
}