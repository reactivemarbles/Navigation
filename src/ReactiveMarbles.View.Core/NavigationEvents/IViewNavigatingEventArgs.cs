// Copyright (c) 2019-2023 ReactiveUI Association Incorporated. All rights reserved.
// ReactiveUI Association Incorporated licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

namespace ReactiveMarbles.View.Core;

/// <summary>
/// IView Model Navigating EventArgs.
/// </summary>
public interface IViewNavigatingEventArgs : IViewNavigationEventArgs
{
    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="IViewNavigatingEventArgs"/> is cancel.
    /// </summary>
    /// <value>
    ///   <c>true</c> if cancel; otherwise, <c>false</c>.
    /// </value>
    bool Cancel { get; set; }
}
