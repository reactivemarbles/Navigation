// Copyright (c) 2019-2023 ReactiveUI Association Incorporated. All rights reserved.
// ReactiveUI Association Incorporated licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

#if MAUIMAC

using System;
using System.Reactive.Disposables;
using CoreFoundation;
using Foundation;
using NSAction = System.Action;

namespace System.Reactive.Concurrency;

/// <summary>
/// MauiMacScheduler.
/// </summary>
public class MauiMacScheduler : IScheduler
{
    /// <inheritdoc/>
    public DateTimeOffset Now => DateTimeOffset.Now;

    /// <inheritdoc/>
    public IDisposable Schedule<TState>(TState state, Func<IScheduler, TState, IDisposable> action)
    {
        var innerDisp = new SingleAssignmentDisposable();

        DispatchQueue.MainQueue.DispatchAsync(new NSAction(() =>
        {
            if (!innerDisp.IsDisposed)
            {
                innerDisp.Disposable = action(this, state);
            }
        }));

        return innerDisp;
    }

    /// <inheritdoc/>
    public IDisposable Schedule<TState>(TState state, DateTimeOffset dueTime, Func<IScheduler, TState, IDisposable> action)
    {
        if (dueTime <= Now)
        {
            return Schedule(state, action);
        }

        return Schedule(state, dueTime - Now, action);
    }

    /// <inheritdoc/>
    public IDisposable Schedule<TState>(TState state, TimeSpan dueTime, Func<IScheduler, TState, IDisposable> action)
    {
        var innerDisp = Disposable.Empty;
        var isCancelled = false;

        var timer = NSTimer.CreateScheduledTimer(dueTime, _ =>
        {
            if (!isCancelled)
            {
                innerDisp = action(this, state);
            }
        });

        return Disposable.Create(() =>
        {
            isCancelled = true;
            timer.Invalidate();
            innerDisp.Dispose();
        });
    }
}
#endif
