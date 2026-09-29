using System.Collections.ObjectModel;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace LazarevFirewall;

public partial class MainWindow : Window
{
    public ObservableCollection<DemoApp> Apps { get; } = new();

    public MainWindow()
    {
        InitializeComponent();
        DataContext = this;
        var workArea = SystemParameters.WorkArea;
        var windowWidth = Math.Min(workArea.Width * 0.80, workArea.Height * 0.80 * 16.0 / 9.0);
        Width = windowWidth;
        Height = windowWidth * 9.0 / 16.0;
        Left = workArea.Left + (workArea.Width - Width) / 2.0;
        Top = workArea.Top + (workArea.Height - Height) / 2.0;
        var dark = Application.Current.Resources.MergedDictionaries[0].Source.OriginalString.Contains("Dark");
        var green = Brush("GreenBrush");
        var red = Brush("RedBrush");
        var amber = Brush("AmberBrush");
        var greenFill = Fill(dark ? 0x40166634 : 0xFFDCFCE7);
        var redFill = Fill(dark ? 0x40991B1B : 0xFFFEE2E2);
        var amberFill = Fill(dark ? 0x40B45309 : 0xFFFEF3C7);
        var greenBorder = Fill(dark ? 0x6622C55E : 0xFF86EFAC);
        var redBorder = Fill(dark ? 0x66EF4444 : 0xFFFDA4AF);
        var amberBorder = Fill(dark ? 0x66D97706 : 0xFFFCD34D);
        var entries = new[]
        {
            new DemoApp("\u25A4", "UnknownApp.exe", "Blocked by default", "2026-09-23 10:44:52", "2026-09-23 10:40:12", @"C:\Users\Public\Downloads\UnknownApp.exe", amber, amberFill, amberBorder, true),
            new DemoApp("\u25CF", "Microsoft Edge", "Allowed", "2026-09-23 10:44:38", "2026-09-23 10:15:00", @"C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe", green, greenFill, greenBorder, false),
            new DemoApp("\u25A6", "Google Chrome", "Allowed", "2026-09-23 10:43:50", "2026-09-23 10:12:20", @"C:\Program Files\Google\Chrome\Application\chrome.exe", green, greenFill, greenBorder, false),
            new DemoApp("\u2699", "TelemetryTool.exe", "Blocked", "2026-09-23 10:43:25", "2026-09-23 10:20:45", @"C:\Tools\TelemetryTool.exe", red, redFill, redBorder, false),
            new DemoApp("\u25C9", "Discord.exe", "Allowed", "2026-09-23 10:41:15", "2026-09-23 10:05:30", @"C:\Users\Admin\AppData\Local\Discord\app-1.0.9012\Discord.exe", green, greenFill, greenBorder, false),
            new DemoApp("\u25A4", "Backup Agent", "Blocked", "2026-09-23 10:38:00", "2026-09-23 09:12:00", @"C:\Program Files\Contoso Backup\BackupAgent.exe", red, redFill, redBorder, false)
        };
        foreach (var entry in entries) Apps.Add(entry);
    }

    private Brush Brush(string key) => (Brush)Application.Current.FindResource(key);
    private static Brush Fill(uint argb) => new SolidColorBrush(Color.FromArgb((byte)(argb >> 24), (byte)(argb >> 16), (byte)(argb >> 8), (byte)argb));

    private void TitleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (e.ClickCount == 2) WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
        else if (e.LeftButton == MouseButtonState.Pressed) DragMove();
    }

    private void Minimize_Click(object sender, RoutedEventArgs e) => WindowState = WindowState.Minimized;
    private void Maximize_Click(object sender, RoutedEventArgs e) => WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
    private void Close_Click(object sender, RoutedEventArgs e) => Close();
}

public sealed class DemoApp
{
    public string Name { get; }
    public string Action { get; }
    public string Last { get; }
    public string Added { get; }
    public string Path { get; }
    public Brush ActionColor { get; }
    public Brush ActionBack { get; }
    public Brush ActionBorder { get; }
    public Brush RowBrush { get; }

    public DemoApp(string icon, string name, string action, string last, string added, string path, Brush color, Brush fill, Brush border, bool selected)
    {
        Name = icon + "  " + name;
        Action = action;
        Last = last;
        Added = added;
        Path = path;
        ActionColor = color;
        ActionBack = fill;
        ActionBorder = border;
        RowBrush = (Brush)Application.Current.FindResource(selected ? "SelectionBrush" : "TableBrush");
    }
}


