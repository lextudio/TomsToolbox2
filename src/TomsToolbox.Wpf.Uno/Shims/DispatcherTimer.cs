namespace System.Windows.Threading;

// WPF System.Windows.Threading.DispatcherTimer shim, backed by the WinUI
// Microsoft.UI.Xaml.DispatcherTimer. Lives in the TomsToolbox.Wpf Uno port
// (not WindowsShims) to avoid colliding with the WindowsShims DataGrid fork,
// which uses the WinUI timer directly. Mirrors the WPF surface so ported code
// compiles and runs.
public sealed class DispatcherTimer
{
    private readonly Microsoft.UI.Xaml.DispatcherTimer _inner = new();

    public DispatcherTimer()
    {
        _inner.Tick += (s, e) => Tick?.Invoke(this, EventArgs.Empty);
    }

    public DispatcherTimer(DispatcherPriority priority) : this()
    {
    }

    public TimeSpan Interval
    {
        get => _inner.Interval;
        set => _inner.Interval = value;
    }

    public bool IsEnabled
    {
        get => _inner.IsEnabled;
        set
        {
            if (value) _inner.Start();
            else _inner.Stop();
        }
    }

    public object? Tag { get; set; }

    public event EventHandler? Tick;

    public void Start() => _inner.Start();

    public void Stop() => _inner.Stop();
}
