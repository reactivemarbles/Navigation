// Copyright (c) 2019-2023 ReactiveUI Association Incorporated. All rights reserved.
// ReactiveUI Association Incorporated licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

namespace ReactiveMarbles.ViewModel.Core;

/// <summary>
/// IAmViewFor.
/// </summary>
/// <typeparam name="T">The type of ViewModel.</typeparam>
public interface IAmViewFor<T> : IAmViewFor
where T : class
{
    /// <summary>
    /// Gets or sets the ViewModel corresponding to this specific View. This should be
    /// a DependencyProperty if you're using XAML.
    /// </summary>
    new T? ViewModel { get; set; }
}
