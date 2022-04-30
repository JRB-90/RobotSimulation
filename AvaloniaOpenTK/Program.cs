using Avalonia;
using Avalonia.Logging;
using Avalonia.OpenGL;
using Avalonia.ReactiveUI;
using System;
using System.Collections.Generic;

namespace AvaloniaOpenTK
{
    internal class Program
    {
        // Initialization code. Don't use any Avalonia, third-party APIs or any
        // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
        // yet and stuff might break.
        [STAThread]
        public static void Main(string[] args) => BuildAvaloniaApp()
            .StartWithClassicDesktopLifetime(args);

        // Avalonia configuration, don't remove; also used by visual designer.
        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UseOpenTK(new List<GlVersion> { new GlVersion(GlProfileType.OpenGL, 3, 0, true) })
                .UsePlatformDetect()
                .LogToTrace(LogEventLevel.Debug)
                .UseReactiveUI()
                .With(new Win32PlatformOptions
                {
                    AllowEglInitialization = true
                });
    }
}
