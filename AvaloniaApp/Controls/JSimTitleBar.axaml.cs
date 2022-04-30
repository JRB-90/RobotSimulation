using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using JSim.Avalonia.ViewModels;
using System;
using System.Runtime.InteropServices;

namespace AvaloniaApp.Controls
{
    public partial class JSimTitleBar : UserControl
    {
        public JSimTitleBar()
        {
            InitializeComponent();

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows) == false)
            {
                throw new ApplicationException("Custom title bar only supported on windows");
            }

            minimizeButton = this.FindControl<Button>("MinimizeButton");
            maximizeButton = this.FindControl<Button>("MaximizeButton");
            closeButton = this.FindControl<Button>("CloseButton");
            windowIcon = this.FindControl<Image>("WindowIcon");

            titleBar = this.FindControl<DockPanel>("TitleBar");
            titleBarBackground = this.FindControl<DockPanel>("TitleBarBackground");
            systemChromeTitle = this.FindControl<TextBlock>("SystemChromeTitle");
            seamlessMenuBar = this.FindControl<NativeMenuBar>("SeamlessMenuBar");
            defaultMenuBar = this.FindControl<NativeMenuBar>("DefaultMenuBar");

            minimizeButton.Click += MinimizeWindow;
            maximizeButton.Click += MaximizeWindow;
            closeButton.Click += CloseWindow;
            windowIcon.DoubleTapped += CloseWindow;
        }

        public static readonly StyledProperty<MainMenuViewModel> MainMenuVMProperty =
            AvaloniaProperty.Register<JSimTitleBar, MainMenuViewModel>(
                nameof(MainMenuVM)
            );

        public MainMenuViewModel MainMenuVM
        {
            get => GetValue(MainMenuVMProperty);
            set => SetValue(MainMenuVMProperty, value);
        }

        public static readonly StyledProperty<bool> IsSeamlessProperty =
            AvaloniaProperty.Register<JSimTitleBar, bool>(
                nameof(IsSeamless)
            );

        public bool IsSeamless
        {
            get => GetValue(IsSeamlessProperty);
            set
            {
                SetValue(IsSeamlessProperty, value);
                if (titleBarBackground != null &&
                    systemChromeTitle != null &&
                    seamlessMenuBar != null &&
                    defaultMenuBar != null)
                {
                    titleBarBackground.IsVisible = IsSeamless ? false : true;
                    systemChromeTitle.IsVisible = IsSeamless ? false : true;
                    seamlessMenuBar.IsVisible = IsSeamless ? true : false;
                    defaultMenuBar.IsVisible = IsSeamless ? false : true;

                    if (IsSeamless == false)
                    {
                        titleBar.Resources["SystemControlForegroundBaseHighBrush"] = 
                            new SolidColorBrush
                            {
                                Color = new Color(255, 0, 0, 0) 
                            };
                    }
                }
            }
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void CloseWindow(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            Window? hostWindow = (Window?)VisualRoot;

            if (hostWindow != null)
            {
                hostWindow.Close();
            }
            else
            {
                throw new ApplicationException("Could not find the visual root");
            }
        }

        private void MaximizeWindow(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            Window? hostWindow = (Window?)VisualRoot;

            if (hostWindow != null)
            {
                if (hostWindow.WindowState == WindowState.Normal)
                {
                    hostWindow.WindowState = WindowState.Maximized;
                }
                else
                {
                    hostWindow.WindowState = WindowState.Normal;
                }
            }
            else
            {
                throw new ApplicationException("Could not find the visual root");
            }
        }

        private void MinimizeWindow(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            Window? hostWindow = (Window?)VisualRoot;

            if (hostWindow != null)
            {
                hostWindow.WindowState = WindowState.Minimized;
            }
            else
            {
                throw new ApplicationException("Could not find the visual root");
            }
        }

        private Button minimizeButton;
        private Button maximizeButton;
        private Button closeButton;
        private Image windowIcon;

        private DockPanel titleBar;
        private DockPanel titleBarBackground;
        private TextBlock systemChromeTitle;
        private NativeMenuBar? seamlessMenuBar;
        private NativeMenuBar? defaultMenuBar;
    }
}
