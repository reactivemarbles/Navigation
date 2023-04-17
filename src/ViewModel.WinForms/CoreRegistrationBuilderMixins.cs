// Copyright (c) 2019-2023 ReactiveUI Association Incorporated. All rights reserved.
// ReactiveUI Association Incorporated licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System.Reactive.Concurrency;
using ReactiveMarbles.Mvvm;

namespace ReactiveMarbles.Locator;

/// <summary>
/// CoreRegistrationBuilderMixins.
/// </summary>
public static class CoreRegistrationBuilderMixins
{
    /// <summary>
    /// Uses the WPF thread schedulers.
    /// </summary>
    /// <param name="builder">The builder.</param>
    /// <returns>The Builder.</returns>
    /// <exception cref="System.ArgumentNullException">builder.</exception>
    public static CoreRegistrationBuilder UseWinFormsThreadSchedulers(this CoreRegistrationBuilder builder)
    {
        if (builder == null)
        {
            throw new ArgumentNullException(nameof(builder));
        }

        return builder.WithMainThreadScheduler(DispatcherScheduler.Current).WithTaskPoolScheduler(TaskPoolScheduler.Default);
    }
}
