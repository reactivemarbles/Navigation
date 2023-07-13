﻿// Copyright (c) 2019-2023 ReactiveUI Association Incorporated. All rights reserved.
// ReactiveUI Association Incorporated licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

namespace ReactiveMarbles.ViewModel.Core;

/// <summary>
/// I View Model Navigation EventArgs.
/// </summary>
public interface IViewModelNavigationEventArgs : IViewModelNavigationBaseEventArgs
{
    /// <summary>
    /// Gets or sets the name of the host.
    /// </summary>
    /// <value>
    /// The name of the host.
    /// </value>
    string HostName { get; set; }

    /// <summary>
    /// Gets the type of the navigation.
    /// </summary>
    /// <value>
    /// The type of the navigation.
    /// </value>
    NavigationType NavigationType { get; }

    /// <summary>
    /// Gets or sets the view.
    /// </summary>
    /// <value>
    /// The view.
    /// </value>
    IAmViewFor? View { get; set; }
}
