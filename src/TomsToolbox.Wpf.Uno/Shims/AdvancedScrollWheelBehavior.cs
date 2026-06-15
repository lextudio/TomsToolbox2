// No-op shim for TomsToolbox.Wpf.Interactivity.AdvancedScrollWheelBehavior.
// The real class uses Microsoft.Xaml.Behaviors (WPF Blend SDK) for smooth scroll-wheel
// animation on DataGrid — not available on Uno. This shim keeps ILSpy Metadata/Helpers.cs
// compiling; animated scroll is a Roma.Host polish milestone.
namespace TomsToolbox.Wpf.Interactivity;

public enum AdvancedScrollWheelMode { WithoutAnimation, WithAnimation }

public static class AdvancedScrollWheelBehavior
{
    public static void SetAttach(DependencyObject element, AdvancedScrollWheelMode value) { }
    public static AdvancedScrollWheelMode GetAttach(DependencyObject element) => AdvancedScrollWheelMode.WithoutAnimation;
}
