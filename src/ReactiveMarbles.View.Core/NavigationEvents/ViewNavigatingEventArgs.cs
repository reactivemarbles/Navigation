// Copyright (c) 2019-2023 ReactiveUI Association Incorporated. All rights reserved.
// ReactiveUI Association Incorporated licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System.Runtime.Serialization;
using ReactiveMarbles.ViewModel.Core;

namespace ReactiveMarbles.View.Core;

/// <summary>
/// View Model Navigating Event Args.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="ViewNavigatingEventArgs" /> class.
/// </remarks>
/// <param name="from">From.</param>
/// <param name="to">To.</param>
/// <param name="navType">Type of the nav.</param>
/// <param name="view">The view.</param>
/// <param name="hostName">The hostName.</param>
/// <param name="parmeter">The parmeter.</param>
[DataContract]
public class ViewNavigatingEventArgs(IRxNavBase? from, IRxNavBase? to, NavigationType navType, IAmViewFor? view, string hostName, object? parmeter = null)
    : ViewNavigationEventArgs(from, to, navType, view, hostName, parmeter), IViewNavigatingEventArgs
{
    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="ViewNavigatingEventArgs"/>
    /// is canceled.
    /// </summary>
    /// <value><c>true</c> if cancel; otherwise, <c>false</c>.</value>
    [DataMember]
    public bool Cancel { get; set; }
}
