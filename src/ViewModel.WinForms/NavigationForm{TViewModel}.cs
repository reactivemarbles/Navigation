﻿// Copyright (c) 2019-2023 ReactiveUI Association Incorporated. All rights reserved.
// ReactiveUI Association Incorporated licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System.ComponentModel;
using ReactiveMarbles.Locator;
using ReactiveMarbles.ObservableEvents;
using ReactiveMarbles.ViewModel.Core;

namespace ReactiveMarbles.ViewModel.WinForms;

/// <summary>
/// NavigationForm.
/// </summary>
/// <typeparam name="TViewModel">The type of the view model.</typeparam>
/// <seealso cref="System.Windows.Forms.Form" />
/// <seealso cref="ReactiveMarbles.ViewModel.Core.IAmViewFor&lt;TViewModel&gt;" />
public partial class NavigationForm<TViewModel> : NavigationForm, IAmViewFor<TViewModel>
where TViewModel : class, IRxObject
{
    /// <summary>
    /// Initializes a new instance of the <see cref="NavigationForm{TViewModel}"/> class.
    /// </summary>
    public NavigationForm()
    {
        InitializeComponent();
        this.Events().Load.Subscribe(_ => ViewModel ??= ServiceLocator.Current().GetService<TViewModel>());
    }

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
