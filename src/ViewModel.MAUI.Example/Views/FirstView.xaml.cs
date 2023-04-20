// Copyright (c) 2019-2023 ReactiveUI Association Incorporated. All rights reserved.
// ReactiveUI Association Incorporated licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using ReactiveMarbles.Locator;

namespace ViewModel.MAUI.Example;

/// <summary>
/// FirstView.
/// </summary>
[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class FirstView
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FirstView"/> class.
    /// </summary>
    public FirstView()
    {
        InitializeComponent();
    }

    /// <summary>
    /// Called when [appearing].
    /// </summary>
    protected override void OnAppearing()
    {
        ViewModel ??= ServiceLocator.Current().GetService<FirstViewModel>();
        GotoMain.Command = ViewModel.GotoMain;
        GotoFirst.Command = ViewModel.GotoFirst;
        base.OnAppearing();
    }
}
