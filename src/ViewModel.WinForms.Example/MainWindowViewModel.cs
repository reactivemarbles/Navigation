// Copyright (c) 2019-2023 ReactiveUI Association Incorporated. All rights reserved.
// ReactiveUI Association Incorporated licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

#if !DESIGN
using System.Runtime.Versioning;
using ReactiveMarbles.Locator;
using ReactiveMarbles.ViewModel.Core;
using ViewModel.WinForms.Example;
using ViewModel.WinForms.Example.Views;
#endif

namespace ViewModel.Wpf.Example;

/// <summary>
/// MainWindowViewModel.
/// </summary>
public class MainWindowViewModel : RxNavBase
{
#if !DESIGN
    /// <summary>
    /// Initializes a new instance of the <see cref="MainWindowViewModel"/> class.
    /// </summary>
    [RequiresPreviewFeatures]
    public MainWindowViewModel()
    {
        ServiceLocator.Current().AddSingleton<MainViewModel>(() => new());
        ServiceLocator.Current().AddNavigationView<MainView, MainViewModel>();

        ServiceLocator.Current().AddSingleton<FirstViewModel>(() => new());
        ServiceLocator.Current().AddNavigationView<FirstView, FirstViewModel>();

        ServiceLocator.Current().SetupComplete();
        var s = new SecondForm();
        s.Show();
    }
#endif
}
