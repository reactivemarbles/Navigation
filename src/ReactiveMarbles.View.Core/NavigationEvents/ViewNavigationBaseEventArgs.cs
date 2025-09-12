// Copyright (c) 2019-2023 ReactiveUI Association Incorporated. All rights reserved.
// ReactiveUI Association Incorporated licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System.Runtime.Serialization;
using ReactiveMarbles.ViewModel.Core;

namespace ReactiveMarbles.View.Core;

/// <summary>
/// View Model Navigation Base Event Args.
/// </summary>
/// <seealso cref="EventArgs" />
[DataContract]
public abstract class ViewNavigationBaseEventArgs
                : EventArgs, IViewNavigationBaseEventArgs
{
    /// <summary>
    /// Gets or sets where is Navigating from.
    /// </summary>
    /// <value>From.</value>
    [DataMember]
    public IRxNavBase? From { get; protected set; }

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
    public IRxNavBase? To { get; protected set; }
}
