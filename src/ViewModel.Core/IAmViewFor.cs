﻿// Copyright (c) 2019-2023 ReactiveUI Association Incorporated. All rights reserved.
// ReactiveUI Association Incorporated licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

namespace ReactiveMarbles.ViewModel.Core;

/// <summary>
/// IAmViewFor.
/// </summary>
/// <seealso cref="ReactiveMarbles.ViewModel.Core.IActivatableView" />
public interface IAmViewFor : IActivatableView
{
    /// <summary>
    /// Gets or sets the View Model associated with the View.
    /// </summary>
    object? ViewModel { get; set; }
}
