// Copyright (c) 2019-2023 ReactiveUI Association Incorporated. All rights reserved.
// ReactiveUI Association Incorporated licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System.Windows;
using ReactiveMarbles.Locator;
using ReactiveMarbles.ObservableEvents;
using ReactiveMarbles.ViewModel.Core;

namespace ReactiveMarbles.ViewModel.Wpf;

/// <summary>
/// Navigation Window.
/// </summary>
/// <typeparam name="TViewModel">The type of the view model.</typeparam>
public class NavigationWindow<TViewModel> : NavigationWindow, IAmViewFor<TViewModel>
    where TViewModel : class, IRxNavBase
{
    /// <summary>
    /// The view model dependency property.
    /// </summary>
    public static readonly DependencyProperty ViewModelProperty =
        DependencyProperty.Register(
            "ViewModel",
            typeof(TViewModel),
            typeof(NavigationWindow<TViewModel>),
            new PropertyMetadata(null));

    /// <summary>
    /// Initializes a new instance of the <see cref="NavigationWindow{TViewModel}"/> class.
    /// </summary>
    public NavigationWindow() =>
        this.Events().Loaded.Subscribe(_ => ViewModel ??= ServiceLocator.Current().GetService<TViewModel>());

    /// <summary>
    /// Gets the binding root view model.
    /// </summary>
    public TViewModel? BindingRoot => ViewModel;

    /// <inheritdoc/>
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
}
