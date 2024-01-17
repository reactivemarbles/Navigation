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
/// FirstView.
/// </summary>
#if DESIGN
public partial class FirstView : UserControl
#else
public partial class FirstView : RxUserControl<FirstViewModel>
#endif
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FirstView"/> class.
    /// </summary>
    [RequiresPreviewFeatures]
    public FirstView()
    {
        InitializeComponent();
#if !DESIGN
        this.Events().Load.Subscribe(_ =>
        {
            ViewModel ??= ServiceLocator.Current().GetService<FirstViewModel>();
            GotoFirst.Command = ViewModel.GotoFirst;
            GotoMain.Command = ViewModel.GotoMain;
        });
#endif
    }
}
