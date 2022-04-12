using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Styling;

namespace JSim.Avalonia.Controls
{
    public class JSimTextBox : TextBox, IStyleable
    {
        public JSimTextBox()
        {
            PropertyChanged += OnPropertyChanged;
            KeyDown += HandleKeyDown;
            LostFocus += HandleFocusLost;
        }

        Type IStyleable.StyleKey => typeof(JSimTextBox);

        public static readonly StyledProperty<string?> ValidatedTextProperty =
            AvaloniaProperty.Register<JSimTextBox, string?>(
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
                    ValidatedText = oldValue;
                }
            }
        }

        private void HandleFocusLost(object? sender, global::Avalonia.Interactivity.RoutedEventArgs e)
        {
            ValidateInput();
        }

        private void HandleKeyDown(object? sender, global::Avalonia.Input.KeyEventArgs e)
        {
            if (e.Key == global::Avalonia.Input.Key.Enter)
            {
                ValidateInput();
            }
        }

        private void ValidateInput()
        {
            ValidatedText = Text;
        }

        private void OnPropertyChanged(object? sender, AvaloniaPropertyChangedEventArgs e)
        {
            if (e.Property == ValidatedTextProperty)
            {
                Text = ValidatedText;
            }
        }
    }
}
