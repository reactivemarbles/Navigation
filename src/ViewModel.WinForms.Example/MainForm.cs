// Copyright (c) 2019-2023 ReactiveUI Association Incorporated. All rights reserved.
// ReactiveUI Association Incorporated licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using ReactiveMarbles.Command;
using ReactiveMarbles.Locator;
using ReactiveMarbles.Mvvm;
using ReactiveMarbles.ObservableEvents;
using ReactiveMarbles.ViewModel.Core;
using ReactiveMarbles.ViewModel.WinForms;
using ViewModel.Wpf.Example;

namespace ViewModel.WinForms.Example;

/// <summary>
/// Form1.
/// </summary>
#if DESIGN
public partial class MainForm : NavigationForm
#else
public partial class MainForm : NavigationForm<MainWindowViewModel>
#endif
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MainForm"/> class.
    /// </summary>
    public MainForm()
    {
#if !DESIGN
        ServiceLocator.Current().AddCoreRegistrations(() =>
        CoreRegistrationBuilder
            .Create()
            .UseWinFormsThreadSchedulers()
            .WithExceptionHandler(new DebugExceptionHandler())
            .Build());
        ServiceLocator.Current().AddSingleton<MainWindowViewModel>(() => new());
#endif
        InitializeComponent();
#if !DESIGN
        this.Events().Load.Subscribe(_ =>
        {
            this.NavigateToView<MainViewModel>();
            NavBack.Command = RxCommand.Create(() => this.NavigateBack(), CanNavigateBack);
        });
#endif
    }
}
