// Copyright (c) 2019-2023 ReactiveUI Association Incorporated. All rights reserved.
// ReactiveUI Association Incorporated licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using ReactiveMarbles.ViewModel.MAUI;

namespace ViewModel.MAUI.Example;

/// <summary>
/// FirstView.
/// </summary>
[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class FirstView : RxContentPage<FirstViewModel>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FirstView"/> class.
    /// </summary>
    public FirstView()
    {
        InitializeComponent();
        ////ViewModel ??= Locator.Current.GetService<FirstViewModel>();
        ////this.BindCommand(ViewModel, vm => vm.GotoMain, v => v.GotoMain).DisposeWith(d);
        ////this.BindCommand(ViewModel, vm => vm.GotoFirst, v => v.GotoFirst).DisposeWith(d);
    }
}
