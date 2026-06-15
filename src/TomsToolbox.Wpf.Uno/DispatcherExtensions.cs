namespace TomsToolbox.Wpf;

using System;
using System.Reflection;
using System.Windows.Threading;

public static class DispatcherExtensions
{
    public static Dispatcher CurrentDispatcher => Dispatcher.CurrentDispatcher;

    // On WinUI/Uno there is no Application.Current.Dispatcher; fall back to CurrentDispatcher.
    public static Dispatcher UIThreadDispatcher => Dispatcher.CurrentDispatcher;

    public static T? Invoke<T>(this Dispatcher? dispatcher, Func<T> method)
        => InternalInvoke(dispatcher, method);

    public static void Invoke(this Dispatcher? dispatcher, Action method)
        => InternalInvoke(dispatcher, method);

    private static T? InternalInvoke<T>(Dispatcher? dispatcher, Func<T> method)
    {
        var result = InternalInvoke(dispatcher, (Delegate)method);
        return result == null ? default : (T)result;
    }

    private static object? InternalInvoke(Dispatcher? dispatcher, Delegate method)
    {
        if (dispatcher == null || dispatcher.CheckAccess())
        {
            try { return method.DynamicInvoke(); }
            catch (Exception ex) { throw UnwrapTargetInvocation(ex); }
        }

        Exception? innerException = null;
        var result = dispatcher.Invoke(() =>
        {
            try { return method.DynamicInvoke(); }
            catch (Exception ex) { innerException = ex; return null; }
        });

        if (innerException != null) throw UnwrapTargetInvocation(innerException);
        return result;
    }

    private static Exception UnwrapTargetInvocation(Exception ex)
        => ex is TargetInvocationException ? ex.InnerException ?? ex : ex;

    public static DispatcherOperation BeginInvoke(this Dispatcher dispatcher, Action method)
        => BeginInvoke(dispatcher, DispatcherPriority.Normal, method);

    public static DispatcherOperation BeginInvoke(this Dispatcher dispatcher, DispatcherPriority priority, Action method)
        => dispatcher.BeginInvoke(method, priority, null);

    public static void Restart(this DispatcherTimer timer)
    {
        timer.Stop();
        timer.Start();
    }
}
