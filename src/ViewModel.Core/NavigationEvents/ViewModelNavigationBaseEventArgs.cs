﻿// Copyright (c) 2019-2023 ReactiveUI Association Incorporated. All rights reserved.
// ReactiveUI Association Incorporated licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System.Runtime.Serialization;

namespace ReactiveMarbles.ViewModel.Core;

/// <summary>
/// View Model Navigation Base Event Args.
/// </summary>
/// <seealso cref="EventArgs" />
[DataContract]
public abstract class ViewModelNavigationBaseEventArgs
                : EventArgs, IViewModelNavigationBaseEventArgs
{
    /// <summary>
    /// Gets or sets where is Navigating from.
    /// </summary>
    /// <value>From.</value>
    [DataMember]
    public IRxObject? From { get; protected set; }

    /// <summary>
    /// Gets or sets the navigation parameter.
    /// </summary>
    /// <value>The navigation parameter.</value>
    [DataMember]
    public object? NavigationParameter { get; protected set; }

    /// <summary>
    /// Gets or sets where is Navigating to.
    /// </summary>
    /// <value>To.</value>
    [DataMember]
    public IRxObject? To { get; protected set; }
}
