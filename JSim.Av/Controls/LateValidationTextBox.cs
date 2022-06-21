using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Styling;

namespace JSim.Av.Controls
{
    public class LateValidationTextBox : TextBox, IStyleable
    {
        Type IStyleable.StyleKey => typeof(TextBox);

        public LateValidationTextBox()
        {
            LostFocus += OnLateValidationFocusLost;
            KeyDown += OnLateValidationKeyDown;
            PropertyChanged += OnPropertyChanged;
        }

        public static readonly StyledProperty<string?> ValidatedTextProperty =
            AvaloniaProperty.Register<LateValidationTextBox, string?>(
                nameof(ValidatedText),
                defaultBindingMode: BindingMode.TwoWay
            );

        public string? ValidatedText
        {
            get => GetValue(ValidatedTextProperty);
            set
            {
                var oldValue = ValidatedText;
                SetValue(ValidatedTextProperty, value);
                if (ValidatedText == null)
                {
                    SetValue(ValidatedTextProperty, oldValue);
                }
            }
        }

        private void OnLateValidationFocusLost(object? sender, RoutedEventArgs e)
        {
            ValidateInput();
        }

        private void OnLateValidationKeyDown(object? sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ValidateInput();
            }
        }

        private void OnPropertyChanged(object? sender, AvaloniaPropertyChangedEventArgs e)
        {
            if (e.Property == ValidatedTextProperty)
            {
                Text = ValidatedText;
            }
        }

        private void ValidateInput()
        {
            ValidatedText = Text;
        }
    }
}
