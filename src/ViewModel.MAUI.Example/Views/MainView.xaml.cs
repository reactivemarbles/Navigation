// Copyright (c) 2019-2023 ReactiveUI Association Incorporated. All rights reserved.
// ReactiveUI Association Incorporated licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using ReactiveMarbles.Locator;
using ReactiveMarbles.ViewModel.Core;

namespace ViewModel.MAUI.Example;

/// <summary>
/// MainView.
/// </summary>
[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class MainView
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MainView" /> class.
    /// </summary>
    public MainView()
    {
        InitializeComponent();
    }

    /// <summary>
    /// Called when [appearing].
    /// </summary>
    protected override void OnAppearing()
    {
        ViewModel ??= ServiceLocator.Current().GetService<MainViewModel>();
        GotoMain.Command = ViewModel.GotoMain;
        GotoFirst.Command = ViewModel.GotoFirst;
        base.OnAppearing();
    }
}
