// Copyright (c) 2019-2023 ReactiveUI Association Incorporated. All rights reserved.
// ReactiveUI Association Incorporated licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System;
using System.Diagnostics;
using System.Reactive.Disposables;
using System.Windows.Input;
using ReactiveMarbles.Command;
using ReactiveMarbles.ViewModel.Core;

namespace ViewModel.Wpf.Example;

#pragma warning disable IDE0071 // Simplify interpolation
/// <summary>
/// MainViewModel.
/// </summary>
public class MainViewModel : RxNavBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MainViewModel"/> class.
    /// </summary>
    public MainViewModel() =>
        this.BuildComplete(() =>
            {
                GotoFirst = RxCommand.Create(() =>
                {
                    this.NavigateToView<MainViewModel>("secondWindow");
                    this.NavigateToView<FirstViewModel>("mainWindow");
                });

                GotoMain = RxCommand.Create(() =>
                {
                    this.NavigateToView<MainViewModel>("mainWindow");
                    this.NavigateToView<FirstViewModel>("secondWindow");
                });
            });

    /// <summary>
    /// Gets the goto first.
    /// </summary>
    /// <value>
    /// The goto first.
    /// </value>
    public ICommand? GotoFirst { get; private set; }

    /// <summary>
    /// Gets the goto main.
    /// </summary>
    /// <value>
    /// The goto main.
    /// </value>
    public ICommand? GotoMain { get; private set; }

    /// <summary>
    /// WhenNavigatedTo.
    /// </summary>
    /// <param name="e"></param>
    /// <param name="disposables"></param>
    /// <inheritdoc />
    public override void WhenNavigatedTo(IViewModelNavigationEventArgs e, CompositeDisposable disposables)
    {
        if (e == null)
        {
            throw new ArgumentNullException(nameof(e));
        }

        Debug.WriteLine($"{DateTime.Now.ToString()} Navigated To: {e.To?.Name} From: {e.From?.Name} with Host {e.HostName}");
        base.WhenNavigatedTo(e, disposables);
    }

    /// <summary>
    /// WhenNavigatedFrom.
    /// </summary>
    /// <param name="e"></param>
    /// <inheritdoc />
    public override void WhenNavigatedFrom(IViewModelNavigationEventArgs e)
    {
        if (e == null)
        {
            throw new ArgumentNullException(nameof(e));
        }

        Debug.WriteLine($"{DateTime.Now.ToString()} Navigated From: {e.From?.Name} To: {e.To?.Name} with Host {e.HostName}");
        base.WhenNavigatedFrom(e);
    }

    /// <summary>
    /// WhenNavigating.
    /// </summary>
    /// <param name="e"></param>
    /// <inheritdoc />
    public override void WhenNavigating(IViewModelNavigatingEventArgs e)
    {
        if (e == null)
        {
            throw new ArgumentNullException(nameof(e));
        }

        Debug.WriteLine($"{DateTime.Now.ToString()} Navigating From: {e.From?.Name!} To: {e.To?.Name!} with Host {e.HostName!}");
        base.WhenNavigating(e);
    }
}
#pragma warning restore IDE0071 // Simplify interpolation

