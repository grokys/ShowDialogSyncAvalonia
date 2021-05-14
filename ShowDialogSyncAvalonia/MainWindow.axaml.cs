using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace ShowDialogSyncAvalonia
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif

            var btn = this.FindControl<Button>("btn");
            btn.Click += (s, e) =>
            {
                System.Diagnostics.Debug.WriteLine("Showing dialog");
                var dialog = new Dialog();
                dialog.ShowDialogSync(this);
                System.Diagnostics.Debug.WriteLine("Dialog closed");
            };
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
