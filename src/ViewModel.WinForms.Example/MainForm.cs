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
/// <seealso cref="System.Windows.Forms.Form" />
public partial class MainForm : NavigationForm<MainWindowViewModel>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MainForm"/> class.
    /// </summary>
    public MainForm()
    {
        ServiceLocator.Current().AddCoreRegistrations(() =>
        CoreRegistrationBuilder
            .Create()
            .UseWinFormsThreadSchedulers()
            .WithExceptionHandler(new DebugExceptionHandler())
            .Build());
        ServiceLocator.Current().AddSingleton<MainWindowViewModel>(() => new());

        InitializeComponent();
        this.Events().Load.Subscribe(_ =>
        {
            this.NavigateToView<MainViewModel>();
            ////NavBack.Command = RxCommand.Create(() => this.NavigateBack(), CanNavigateBack);
        });
    }
}
