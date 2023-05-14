// Copyright (c) 2019-2023 ReactiveUI Association Incorporated. All rights reserved.
// ReactiveUI Association Incorporated licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using ReactiveMarbles.Locator;
using ReactiveMarbles.ViewModel.Core;

namespace ViewModel.Wpf.Example;

/// <summary>
/// MainWindowViewModel.
/// </summary>
public class MainWindowViewModel : RxNavBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MainWindowViewModel"/> class.
    /// </summary>
    public MainWindowViewModel()
    {
        ServiceLocator.Current().AddSingleton<MainViewModel>(() => new());
        ServiceLocator.Current().AddNavigationView<MainView, MainViewModel>();

        ServiceLocator.Current().AddSingleton<FirstViewModel>(() => new());
        ServiceLocator.Current().AddNavigationView<FirstView, FirstViewModel>();

        ServiceLocator.Current().SetupComplete();
        var s = new SecondWindow();
        s.Show();
    }
}
