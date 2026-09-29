using System;
using System.Linq;
using System.Windows;

namespace LazarevFirewall;

public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        var arg = e.Args.Select((x, i) => (x, i)).FirstOrDefault(p => p.x.Equals("--theme", StringComparison.OrdinalIgnoreCase));
        var theme = arg.x is not null && arg.i + 1 < e.Args.Length ? e.Args[arg.i + 1] : "light";
        Resources.MergedDictionaries.Clear();
        Resources.MergedDictionaries.Add(new ResourceDictionary { Source = new Uri(theme.Equals("dark", StringComparison.OrdinalIgnoreCase) ? "Themes/Dark.xaml" : "Themes/Light.xaml", UriKind.Relative) });
        var window = new MainWindow();
        MainWindow = window;
        window.Show();
    }
}
