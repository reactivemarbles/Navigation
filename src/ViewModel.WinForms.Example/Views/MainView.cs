// Copyright (c) 2019-2023 ReactiveUI Association Incorporated. All rights reserved.
// ReactiveUI Association Incorporated licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using ReactiveMarbles.Locator;
using ReactiveMarbles.ObservableEvents;
using ReactiveMarbles.ViewModel.WinForms;

namespace ViewModel.WinForms.Example.Views
{
    /// <summary>
    /// MainView.
    /// </summary>
    public partial class MainView : RxUserControl<MainViewModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainView"/> class.
        /// </summary>
        public MainView()
        {
            InitializeComponent();
            this.Events().Load.Subscribe(_ =>
            {
                ViewModel ??= ServiceLocator.Current().GetService<MainViewModel>();
                ////GotoFirst.Command = ViewModel.GotoFirst;
                ////GotoMain.Command = ViewModel.GotoMain;
            });
        }
    }
}
