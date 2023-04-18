// Copyright (c) 2019-2023 ReactiveUI Association Incorporated. All rights reserved.
// ReactiveUI Association Incorporated licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System;
using ReactiveMarbles.Locator;
using ReactiveMarbles.ViewModel.Core;

namespace ReactiveMarbles.ViewModel.MAUI;

/// <summary>
/// ReactiveShellContent.
/// </summary>
/// <typeparam name="TViewModel">The type of the view model.</typeparam>
/// <seealso cref="ShellContent" />
public class RxShellContent<TViewModel> : ShellContent
    where TViewModel : class
{
    /// <summary>
    /// The contract property.
    /// </summary>
    public static readonly BindableProperty ContractProperty = BindableProperty.Create(
     nameof(Contract),
     typeof(string),
     typeof(RxShellContent<TViewModel>),
     null,
     BindingMode.Default,
     propertyChanged: ViewModelChanged);

    /// <summary>
    /// The view model property.
    /// </summary>
    public static readonly BindableProperty ViewModelProperty = BindableProperty.Create(
     nameof(ViewModel),
     typeof(TViewModel),
     typeof(RxShellContent<TViewModel>),
     default(TViewModel),
     BindingMode.Default,
     propertyChanged: ViewModelChanged);

    /// <summary>
    /// Initializes a new instance of the <see cref="RxShellContent{TViewModel}" /> class.
    /// </summary>
    public RxShellContent()
    {
        var view = ServiceLocator.Current().GetServiceWithContract<IAmViewFor<TViewModel>>(Contract);
        if (view is not null)
        {
            ContentTemplate = new DataTemplate(() => view);
        }
    }

    /// <summary>
    /// Gets or sets the view model.
    /// </summary>
    /// <value>
    /// The view model.
    /// </value>
    public TViewModel? ViewModel
    {
        get => (TViewModel)GetValue(ViewModelProperty);
        set => SetValue(ViewModelProperty, value);
    }

    /// <summary>
    /// Gets or sets the contract for the view.
    /// </summary>
    /// <value>
    /// The contract.
    /// </value>
    public string? Contract
    {
        get => (string?)GetValue(ContractProperty);
        set => SetValue(ContractProperty, value);
    }

    private static void ViewModelChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is RxShellContent<TViewModel> svm)
        {
            var view = ServiceLocator.Current().GetServiceWithContract<IAmViewFor<TViewModel>>(svm.Contract);

            if (view is not null)
            {
                svm.ContentTemplate = new DataTemplate(() => view);
            }
        }
    }
}
