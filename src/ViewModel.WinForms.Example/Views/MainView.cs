// Copyright (c) 2019-2023 ReactiveUI Association Incorporated. All rights reserved.
// ReactiveUI Association Incorporated licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

#if !DESIGN
using System.Runtime.Versioning;
using ReactiveMarbles.Locator;
using ReactiveMarbles.ObservableEvents;
using ReactiveMarbles.ViewModel.WinForms;
#endif

namespace ViewModel.WinForms.Example.Views;

/// <summary>
/// MainView.
/// </summary>
#if DESIGN
public partial class MainView : UserControl
#else
public partial class MainView : RxUserControl<MainViewModel>
#endif
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MainView"/> class.
    /// </summary>
    [RequiresPreviewFeatures]
    public MainView()
    {
        InitializeComponent();
#if !DESIGN
        this.Events().Load.Subscribe(_ =>
        {
            ViewModel ??= ServiceLocator.Current().GetService<MainViewModel>();
            GotoFirst.Command = ViewModel.GotoFirst;
            GotoMain.Command = ViewModel.GotoMain;
        });
#endif
    }
}
