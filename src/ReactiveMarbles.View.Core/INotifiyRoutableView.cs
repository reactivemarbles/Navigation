// Copyright (c) 2019-2023 ReactiveUI Association Incorporated. All rights reserved.
// ReactiveUI Association Incorporated licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System.Reactive.Disposables;
using ReactiveMarbles.ViewModel.Core;

namespace ReactiveMarbles.View.Core;

/// <summary>
/// INotifiy Routable ViewModel.
/// </summary>
/// <seealso cref="IUseHostedNavigation" />
public interface INotifiyRoutableView : Mvvm.IRxObject, IUseHostedNavigation
{
    /// <summary>
    /// Gets the name.
    /// </summary>
    /// <value>
    /// The name.
    /// </value>
    string? Name { get; }

    /// <summary>
    /// Raises the <see cref="E:NavigatedFrom"/> event.
    /// </summary>
    /// <param name="e">
    /// The <see cref="IViewNavigationEventArgs"/> instance containing the event data.
    /// </param>
    void WhenNavigatedFrom(IViewNavigationEventArgs e);

    /// <summary>
    /// Raises the <see cref="E:NavigatedTo"/> event.
    /// </summary>
    /// <param name="e">
    /// The <see cref="IViewNavigationEventArgs"/> instance containing the event data.
    /// </param>
    /// <param name="disposables">The disposables.</param>
    void WhenNavigatedTo(IViewNavigationEventArgs e, CompositeDisposable disposables);

    /// <summary>
    /// Raises the <see cref="E:Navigating"/> event.
    /// </summary>
    /// <param name="e">
    /// The <see cref="IViewNavigatingEventArgs"/> instance containing the event data.
    /// </param>
    void WhenNavigating(IViewNavigatingEventArgs e);
}
