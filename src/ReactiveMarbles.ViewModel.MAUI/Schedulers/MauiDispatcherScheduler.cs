// Copyright (c) 2019-2023 ReactiveUI Association Incorporated. All rights reserved.
// ReactiveUI Association Incorporated licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

#if WINUI || MAUIMAC

namespace System.Reactive.Concurrency;

/// <summary>
/// MauiWinUIDispatcherScheduler.
/// </summary>
public class MauiDispatcherScheduler : IScheduler
{
    private readonly Func<IScheduler> _schedulerFactory;
    private IScheduler? _scheduler;

    /// <summary>
    /// Initializes a new instance of the <see cref="MauiDispatcherScheduler"/> class.
    /// </summary>
    /// <param name="schedulerFactory">A func which will return a new scheduler.</param>
    public MauiDispatcherScheduler(Func<IScheduler> schedulerFactory)
    {
        _schedulerFactory = schedulerFactory;
        AttemptToCreateScheduler();
    }

    /// <inheritdoc/>
    public DateTimeOffset Now => AttemptToCreateScheduler().Now;

    /// <inheritdoc/>
    public IDisposable Schedule<TState>(TState state, Func<IScheduler, TState, IDisposable> action) =>
        AttemptToCreateScheduler().Schedule(state, action);

    /// <inheritdoc/>
    public IDisposable Schedule<TState>(TState state, TimeSpan dueTime, Func<IScheduler, TState, IDisposable> action) => // TODO: Create Test
        AttemptToCreateScheduler().Schedule(state, dueTime, action);

    /// <inheritdoc/>
    public IDisposable Schedule<TState>(TState state, DateTimeOffset dueTime, Func<IScheduler, TState, IDisposable> action) => // TODO: Create Test
        AttemptToCreateScheduler().Schedule(state, dueTime, action);

    private IScheduler AttemptToCreateScheduler()
    {
        if (_scheduler is not null)
        {
            return _scheduler;
        }

        try
        {
            _scheduler = _schedulerFactory();
            return _scheduler;
        }
        catch (InvalidOperationException)
        {
            return CurrentThreadScheduler.Instance;
        }
        catch (ArgumentNullException)
        {
            return CurrentThreadScheduler.Instance;
        }
    }
}
#endif
