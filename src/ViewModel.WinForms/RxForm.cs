﻿// Copyright (c) 2019-2023 ReactiveUI Association Incorporated. All rights reserved.
// ReactiveUI Association Incorporated licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System.ComponentModel;
using ReactiveMarbles.ViewModel.Core;

namespace ReactiveMarbles.ViewModel.WinForms;

/// <summary>
/// RxForm.
/// </summary>
/// <typeparam name="TViewModel">The type of the view model.</typeparam>
public partial class RxForm<TViewModel> : Form, IAmViewFor<TViewModel>
where TViewModel : class, IRxNavBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RxForm{TViewModel}"/> class.
    /// </summary>
    public RxForm() => InitializeComponent();

    /// <inheritdoc/>
    [Category("ReactiveMarbles")]
    [Description("The ViewModel.")]
    [Bindable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public TViewModel? ViewModel { get; set; }

    /// <inheritdoc/>
    object? IAmViewFor.ViewModel
    {
        get => ViewModel;
        set => ViewModel = (TViewModel?)value;
    }
}
