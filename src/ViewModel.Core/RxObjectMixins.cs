// Copyright (c) 2019-2023 ReactiveUI Association Incorporated. All rights reserved.
// ReactiveUI Association Incorporated licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using ReactiveMarbles.Locator;

namespace ReactiveMarbles.ViewModel.Core;

/// <summary>
/// RxObjectMixins.
/// </summary>
public static class RxObjectMixins
{
    private static readonly ReplaySubject<Unit> _buildCompleteSubject = new(1);

    /// <summary>
    /// Sets the IOC container build complete, Execute this once after completion of IOC registrations.
    /// </summary>
    /// <param name="dummy">The dummy.</param>
#pragma warning disable RCS1175 // Unused 'this' parameter.
    public static void SetupComplete(this IEditServices dummy) => _buildCompleteSubject.OnNext(Unit.Default);

    /// <summary>
    /// Gets the build complete.
    /// </summary>
    /// <param name="dummy">The dummy.</param>
    /// <param name="action">The action.</param>
    /// <value>The build complete.</value>
    public static void BuildComplete(this IAmBuilt dummy, Action action) => _buildCompleteSubject.Subscribe(_ => action());
#pragma warning restore RCS1175 // Unused 'this' parameter.
}
