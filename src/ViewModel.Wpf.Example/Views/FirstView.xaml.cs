// Copyright (c) 2019-2023 ReactiveUI Association Incorporated. All rights reserved.
// ReactiveUI Association Incorporated licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System;
using ReactiveMarbles.Locator;
using ReactiveMarbles.ObservableEvents;

namespace ViewModel.Wpf.Example
{
    /// <summary>
    /// Interaction logic for MainView.xaml.
    /// </summary>
    public partial class FirstView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FirstView"/> class.
        /// </summary>
        public FirstView()
        {
            InitializeComponent();
            this.Events().Loaded.Subscribe(_ =>
            {
                ViewModel ??= ServiceLocator.Current().GetService<FirstViewModel>();
                GotoFirst.Command = ViewModel.GotoFirst;
                GotoMain.Command = ViewModel.GotoMain;
            });
        }
    }
}
