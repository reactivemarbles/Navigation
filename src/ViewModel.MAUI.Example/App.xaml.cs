// Copyright (c) 2019-2023 ReactiveUI Association Incorporated. All rights reserved.
// ReactiveUI Association Incorporated licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using ReactiveMarbles.Locator;
using ReactiveMarbles.Mvvm;
using ReactiveMarbles.ViewModel.Core;

namespace ViewModel.MAUI.Example;

/// <summary>
/// App.
/// </summary>
/// <seealso cref="Microsoft.Maui.Controls.Application" />
public partial class App : Application
{
    /// <summary>
    /// Initializes a new instance of the <see cref="App"/> class.
    /// </summary>
    /// <remarks>
    /// To be added.
    /// </remarks>
    public App()
    {
        InitializeComponent();
        ServiceLocator.Current().AddCoreRegistrations(() =>
        CoreRegistrationBuilder
            .Create()
            .UseMauiThreadSchedulers()
            .WithExceptionHandler(new DebugExceptionHandler())
            .Build());
        ServiceLocator.Current().AddSingleton<MainViewModel>(() => new());
        ServiceLocator.Current().AddNavigationView<MainView, MainViewModel>();

        ServiceLocator.Current().AddSingleton<FirstViewModel>(() => new());
        ServiceLocator.Current().AddNavigationView<FirstView, FirstViewModel>();

        MainPage = new AppShell();
        ServiceLocator.Current().SetupComplete();
    }
}
