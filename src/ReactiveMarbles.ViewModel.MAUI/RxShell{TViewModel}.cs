// Copyright (c) 2019-2023 ReactiveUI Association Incorporated. All rights reserved.
// ReactiveUI Association Incorporated licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using ReactiveMarbles.ViewModel.Core;

namespace ReactiveUI.Maui;

/// <summary>
/// ReactiveShell.
/// </summary>
/// <typeparam name="TViewModel">The type of the view model.</typeparam>
/// <seealso cref="Microsoft.Maui.Controls.Shell" />
public class RxShell<TViewModel> : Shell, IAmViewFor<TViewModel>
    where TViewModel : class
{
    /// <summary>
    /// The view model bindable property.
    /// </summary>
    public static readonly BindableProperty ViewModelProperty = BindableProperty.Create(
     nameof(ViewModel),
     typeof(TViewModel),
     typeof(RxShell<TViewModel>),
     default(TViewModel),
     BindingMode.OneWay,
     propertyChanged: OnViewModelChanged);

    /// <summary>
    /// Gets or sets the ViewModel to display.
    /// </summary>
    public TViewModel? ViewModel
    {
        get => (TViewModel)GetValue(ViewModelProperty);
        set => SetValue(ViewModelProperty, value);
    }

    /// <inheritdoc/>
    object? IAmViewFor.ViewModel
    {
        get => ViewModel;
        set => ViewModel = (TViewModel?)value;
    }

    /// <inheritdoc/>
    protected override void OnBindingContextChanged()
    {
        base.OnBindingContextChanged();
        ViewModel = BindingContext as TViewModel;
    }

    private static void OnViewModelChanged(BindableObject bindableObject, object oldValue, object newValue) => bindableObject.BindingContext = newValue;
}
