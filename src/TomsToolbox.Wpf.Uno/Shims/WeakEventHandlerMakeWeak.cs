namespace WeakEventHandler;

// No-op stub — the WeakEventHandler.Fody weaver is not active under Uno.Sdk.
// The [MakeWeak] attribute is referenced by ObservableObjectBase.RelayedEvents.cs
// so it must compile; runtime behavior is unchanged (strong reference, no leak risk
// in Roma's single-assembly-browser use case).
[AttributeUsage(AttributeTargets.Method)]
public sealed class MakeWeakAttribute : Attribute { }
