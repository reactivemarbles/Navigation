// Copyright (c) 2019-2023 ReactiveUI Association Incorporated. All rights reserved.
// ReactiveUI Association Incorporated licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System;
using ReactiveMarbles.Command;
using ReactiveMarbles.ObservableEvents;
using ReactiveMarbles.ViewModel.Core;

namespace ViewModel.Wpf.Example;

/// <summary>
/// Interaction logic for MainWindow.xaml.
/// </summary>
public partial class MainWindow
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MainWindow"/> class.
    /// </summary>
    public MainWindow()
    {
        InitializeComponent();
        this.Events().Loaded.Subscribe(_ =>
        {
            this.NavigateToView<MainViewModel>();
            NavBack.Command = RxCommand.Create(() => this.NavigateBack(), CanNavigateBack);
        });
    }
}
