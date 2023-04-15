// Copyright (c) 2019-2023 ReactiveUI Association Incorporated. All rights reserved.
// ReactiveUI Association Incorporated licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System;
using System.Reactive.Disposables;
using ReactiveMarbles.Command;
using ReactiveMarbles.ObservableEvents;
using ReactiveMarbles.ViewModel.Core;

namespace ViewModel.Wpf.Example
{
    /// <summary>
    /// Interaction logic for SecondWindow.xaml.
    /// </summary>
    public partial class SecondWindow : IUseNavigation
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SecondWindow"/> class.
        /// </summary>
        public SecondWindow()
        {
            InitializeComponent();
            this.Events().Loaded.Subscribe(_ =>
            {
                this.NavigateToView<FirstViewModel>();
                NavBack.Command = RxCommand.Create(() => this.NavigateBack(), CanNavigateBack);
            });
        }
    }
}
