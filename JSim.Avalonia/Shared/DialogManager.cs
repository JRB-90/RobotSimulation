using Avalonia.Controls;

namespace JSim.Avalonia.Shared
{
    /// <summary>
    /// Provides a mechanism for delivering dialogs in an MVVM compliant manor.
    /// </summary>
    public class DialogManager
    {
        readonly Window window;

        public DialogManager(Window window)
        {
            this.window = window;
        }

        public async Task<string?> ShowSaveFileDialog(
            string title,
            List<FileDialogFilter> filters)
        {
            var saveFileDailog =
                new SaveFileDialog()
                {
                    Title = title,
                    Filters = filters,
                };

            var result = await saveFileDailog.ShowAsync(window);

            return result;
        }

        public async Task<string[]?> ShowOpenFileDialog(
            string title,
            List<FileDialogFilter> filters,
            bool allowMultiple = false)
        {
            var openFileDailog =
                new OpenFileDialog()
                {
                    Title = title,
                    Filters = filters,
                    AllowMultiple = allowMultiple,
                };

            var result = await openFileDailog.ShowAsync(window);

            return result;
        }
    }
}
