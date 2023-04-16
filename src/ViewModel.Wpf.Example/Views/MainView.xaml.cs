// Copyright (c) 2019-2023 ReactiveUI Association Incorporated. All rights reserved.
// ReactiveUI Association Incorporated licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System;
using ReactiveMarbles.Locator;
using ReactiveMarbles.ObservableEvents;
using ReactiveMarbles.ViewModel.Core;

namespace ViewModel.Wpf.Example
{
    /// <summary>
    /// Interaction logic for MainView.xaml.
    /// </summary>
    public partial class MainView : IAmViewFor<MainViewModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainView"/> class.
        /// </summary>
        public MainView()
        {
            InitializeComponent();
            this.Events().Loaded.Subscribe(_ =>
            {
                ViewModel ??= ServiceLocator.Current().GetService<MainViewModel>();
                GotoFirst.Command = ViewModel.GotoFirst;
                GotoMain.Command = ViewModel.GotoMain;
            });
        }
    }
}
