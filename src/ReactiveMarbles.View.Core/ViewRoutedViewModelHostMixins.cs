﻿// Copyright (c) 2019-2023 ReactiveUI Association Incorporated. All rights reserved.
// ReactiveUI Association Incorporated licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Runtime.CompilerServices;
using ReactiveMarbles.Extensions;
using ReactiveMarbles.ViewModel.Core;

[assembly: InternalsVisibleTo("ReactiveMarbles.View.Wpf")]
[assembly: InternalsVisibleTo("ReactiveMarbles.View.WinForms")]
[assembly: InternalsVisibleTo("ReactiveMarbles.View.MAUI")]
[assembly: InternalsVisibleTo("ReactiveMarbles.View.Avalonia")]

namespace ReactiveMarbles.View.Core;

/// <summary>
/// View Model Routed View Host Mixins.
/// </summary>
public static class ViewRoutedViewModelHostMixins
{
#pragma warning disable RCS1175 // Unused 'this' parameter.

    internal static ReplaySubject<Unit> ASetupCompleted { get; } = new(1);

    internal static Dictionary<string, CompositeDisposable> CurrentViewDisposable { get; } = [];

    internal static Dictionary<string, IViewRoutedViewModelHost> NavigationHost { get; } = [];

    internal static Dictionary<string, Subject<IViewNavigatingEventArgs>> ResultNavigating { get; } = [];

    internal static Subject<IViewNavigationEventArgs> SetWhenNavigated { get; } = new();

    internal static Subject<IViewNavigatingEventArgs> SetWhenNavigating { get; } = new();

    internal static Dictionary<string, ReplaySubject<bool>> WhenSetupSubjects { get; } = [];

    /// <summary>
    /// Determines whether this instance [can navigate back] the specified this.
    /// </summary>
    /// <param name="this">The this.</param>
    /// <returns>
    /// A bool.
    /// </returns>
    /// <exception cref="System.ArgumentNullException">this.</exception>
    public static IObservable<bool> CanNavigateBack(this IUseNavigation @this)
    {
        if (@this == null)
        {
            throw new ArgumentNullException(nameof(@this));
        }

        return Observable.Create<bool>(obs =>
        {
            var dis = new CompositeDisposable();
            @this.WhenSetup().Subscribe(_ =>
            {
                if (NavigationHost.Count > 0 && @this.Name != null)
                {
                    if (@this.Name.Length == 0)
                    {
                        NavigationHost.First().Value.CanNavigateBackObservable
                         .Subscribe(x => obs.OnNext(x))
                         .DisposeWith(dis);
                    }

                    if (NavigationHost.TryGetValue(@this.Name, out var value))
                    {
                        value.CanNavigateBackObservable
                         .Subscribe(x => obs.OnNext(x))
                         .DisposeWith(dis);
                    }
                }
            });

            obs.OnNext(false);

            return dis;
        });
    }

    /// <summary>
    /// Determines whether this instance [can navigate back] the specified host name.
    /// </summary>
    /// <param name="this">The navigation host.</param>
    /// <param name="hostName">Name of the host.</param>
    /// <returns>
    /// A bool.
    /// </returns>
    /// <exception cref="System.ArgumentNullException">this.</exception>
    public static IObservable<bool> CanNavigateBack(this IUseHostedNavigation @this, string hostName = "")
    {
        if (@this == null)
        {
            throw new ArgumentNullException(nameof(@this));
        }

        return Observable.Create<bool>(obs =>
         {
             var dis = new CompositeDisposable();
             @this.WhenSetup(hostName).Subscribe(_ =>
             {
                 if (NavigationHost.Count > 0 && hostName != null)
                 {
                     if (hostName.Length == 0)
                     {
                         NavigationHost.First().Value.CanNavigateBackObservable
                         .DistinctUntilChanged()
                         .Subscribe(x => obs.OnNext(x))
                         .DisposeWith(dis);
                     }
                     else if (NavigationHost.TryGetValue(hostName, out var value))
                     {
                         value.CanNavigateBackObservable
                         .DistinctUntilChanged()
                         .Subscribe(x => obs.OnNext(x))
                         .DisposeWith(dis);
                     }
                 }
             }).DisposeWith(dis);

             obs.OnNext(false);

             return dis;
         });
    }

    /// <summary>
    /// Clears the history.
    /// </summary>
    /// <param name="this">The dummy.</param>
    /// <returns>
    /// Chainable host.
    /// </returns>
    /// <exception cref="System.ArgumentNullException">this.</exception>
    /// <exception cref="System.InvalidOperationException">No navigation host registered, please ensure that the NavigationShell has a Name.</exception>
    public static IUseNavigation ClearHistory(this IUseNavigation @this)
    {
        if (@this == null)
        {
            throw new ArgumentNullException(nameof(@this));
        }

        if (NavigationHost.Count == 0)
        {
            throw new InvalidOperationException("No navigation host registered, please ensure that the NavigationShell has a Name.");
        }

        if (NavigationHost.Count > 0 && @this.Name != null)
        {
            switch (@this.Name.Length)
            {
                case 0:
                    NavigationHost.First().Value.ClearHistory();
                    break;
                default:
                    NavigationHost[@this.Name].ClearHistory();
                    break;
            }
        }

        return @this;
    }

    /// <summary>
    /// Clears the history.
    /// </summary>
    /// <param name="this">The this.</param>
    /// <param name="hostName">Name of the host.</param>
    /// <returns>Chainable host.</returns>
    /// <exception cref="System.InvalidOperationException">No navigation host registered, please ensure that the NavigationShell has a Name.</exception>
    public static IUseHostedNavigation ClearHistory(this IUseHostedNavigation @this, string hostName = "")
    {
        if (NavigationHost.Count == 0)
        {
            throw new InvalidOperationException("No navigation host registered, please ensure that the NavigationShell has a Name.");
        }

        if (NavigationHost.Count > 0 && hostName != null)
        {
            switch (hostName.Length)
            {
                case 0:
                    NavigationHost.First().Value.ClearHistory();
                    break;
                default:
                    NavigationHost[hostName].ClearHistory();
                    break;
            }
        }

        return @this;
    }

    /// <summary>
    /// Navigates the back.
    /// </summary>
    /// <param name="this">The this.</param>
    /// <param name="parameter">The parameter.</param>
    /// <returns>
    /// Chainable host.
    /// </returns>
    /// <exception cref="System.ArgumentNullException">this.</exception>
    /// <exception cref="System.InvalidOperationException">No navigation host registered, please ensure that the NavigationShell has a Name.</exception>
    public static IUseNavigation NavigateBack(this IUseNavigation @this, object? parameter = null)
    {
        if (@this == null)
        {
            throw new ArgumentNullException(nameof(@this));
        }

        if (NavigationHost.Count == 0)
        {
            throw new InvalidOperationException("No navigation host registered, please ensure that the NavigationShell has a Name.");
        }

        if (NavigationHost.Count > 0 && @this.Name != null)
        {
            switch (@this.Name.Length)
            {
                case 0:
                    NavigationHost.First().Value.NavigateBack(parameter);
                    break;
                default:
                    if (NavigationHost.TryGetValue(@this.Name, out var value))
                    {
                        value.NavigateBack(parameter);
                    }

                    break;
            }
        }

        return @this;
    }

    /// <summary>
    /// Navigates backwards.
    /// </summary>
    /// <param name="this">The this.</param>
    /// <param name="hostName">Name of the host.</param>
    /// <param name="parameter">The parameter.</param>
    /// <returns>Chainable host.</returns>
    /// <exception cref="System.InvalidOperationException">No navigation host registered, please ensure that the NavigationShell has a Name.</exception>
    public static IUseHostedNavigation NavigateBack(this IUseHostedNavigation @this, string hostName = "", object? parameter = null)
    {
        if (NavigationHost.Count == 0)
        {
            throw new InvalidOperationException("No navigation host registered, please ensure that the NavigationShell has a Name.");
        }

        if (NavigationHost.Count > 0 && hostName != null)
        {
            switch (hostName.Length)
            {
                case 0:
                    NavigationHost.First().Value.NavigateBack(parameter);
                    break;
                default:
                    if (NavigationHost.TryGetValue(hostName, out var value))
                    {
                        value.NavigateBack(parameter);
                    }

                    break;
            }
        }

        return @this;
    }

    /// <summary>
    /// Navigates the specified contract.
    /// </summary>
    /// <typeparam name="T">The Type.</typeparam>
    /// <param name="this">The this.</param>
    /// <param name="contract">The contract.</param>
    /// <param name="parameter">The parameter.</param>
    /// <returns>Chainable host.</returns>
    /// <exception cref="System.ArgumentNullException">this.</exception>
    /// <exception cref="System.InvalidOperationException">No navigation host registered, please ensure that the NavigationShell has a Name.</exception>
    public static IUseNavigation NavigateToView<T>(this IUseNavigation @this, string? contract = null, object? parameter = null)
        where T : class, IRxNavBase
    {
        if (@this == null)
        {
            throw new ArgumentNullException(nameof(@this));
        }

        if (NavigationHost.Count == 0)
        {
            throw new InvalidOperationException("No navigation host registered, please ensure that the NavigationShell has a Name.");
        }

        if (NavigationHost.Count > 0 && @this.Name != null)
        {
            switch (@this.Name.Length)
            {
                case 0:
                    NavigationHost.First().Value.Navigate<IAmViewFor<T>>(contract, parameter);
                    break;
                default:
                    NavigationHost[@this.Name].Navigate<IAmViewFor<T>>(contract, parameter);
                    break;
            }
        }

        return @this;
    }

    /// <summary>
    /// Navigates to view.
    /// </summary>
    /// <typeparam name="T">The Type.</typeparam>
    /// <param name="this">The this.</param>
    /// <param name="hostName">Name of the host.</param>
    /// <param name="contract">The contract.</param>
    /// <param name="parameter">The parameter.</param>
    /// <returns>Chainable host.</returns>
    /// <exception cref="System.InvalidOperationException">No navigation host registered, please ensure that the NavigationShell has a Name.</exception>
    public static IUseHostedNavigation NavigateToView<T>(this IUseHostedNavigation @this, string? hostName = "", string? contract = null, object? parameter = null)
        where T : class, IRxNavBase
    {
        if (NavigationHost.Count == 0)
        {
            throw new InvalidOperationException("No navigation host registered, please ensure that the NavigationShell has a Name.");
        }

        if (NavigationHost.Count > 0 && hostName != null)
        {
            switch (hostName.Length)
            {
                case 0:
                    NavigationHost.First().Value.Navigate<IAmViewFor<T>>(contract, parameter);
                    break;
                default:
                    if (NavigationHost.TryGetValue(hostName, out var value))
                    {
                        value.Navigate<IAmViewFor<T>>(contract, parameter);
                    }

                    break;
            }
        }

        return @this;
    }

    /// <summary>
    /// Navigates the and reset.
    /// </summary>
    /// <typeparam name="T">The Type.</typeparam>
    /// <param name="this">The this.</param>
    /// <param name="contract">The contract.</param>
    /// <param name="parameter">The parameter.</param>
    /// <returns>Chainable host.</returns>
    /// <exception cref="System.ArgumentNullException">this.</exception>
    /// <exception cref="System.InvalidOperationException">No navigation host registered, please ensure that the NavigationShell has a Name.</exception>
    public static IUseNavigation NavigateToViewAndClearHistory<T>(this IUseNavigation @this, string? contract = null, object? parameter = null)
        where T : class, IRxNavBase
    {
        if (@this == null)
        {
            throw new ArgumentNullException(nameof(@this));
        }

        if (NavigationHost.Count == 0)
        {
            throw new InvalidOperationException("No navigation host registered, please ensure that the NavigationShell has a Name.");
        }

        if (NavigationHost.Count > 0 && @this.Name != null)
        {
            switch (@this.Name.Length)
            {
                case 0:
                    NavigationHost.First().Value.NavigateAndReset<IAmViewFor<T>>(contract, parameter);
                    break;
                default:
                    if (NavigationHost.TryGetValue(@this.Name, out var value))
                    {
                        value.NavigateAndReset<IAmViewFor<T>>(contract, parameter);
                    }

                    break;
            }
        }

        return @this;
    }

    /// <summary>
    /// Navigates to view and clear history.
    /// </summary>
    /// <typeparam name="T">The Type.</typeparam>
    /// <param name="this">The this.</param>
    /// <param name="hostName">Name of the host.</param>
    /// <param name="contract">The contract.</param>
    /// <param name="parameter">The parameter.</param>
    /// <returns>Chainable host.</returns>
    /// <exception cref="System.InvalidOperationException">No navigation host registered, please ensure that the NavigationShell has a Name.</exception>
    public static IUseHostedNavigation NavigateToViewAndClearHistory<T>(this IUseHostedNavigation @this, string hostName = "", string? contract = null, object? parameter = null)
        where T : class, IRxNavBase
    {
        if (NavigationHost.Count == 0)
        {
            throw new InvalidOperationException("No navigation host registered, please ensure that the NavigationShell has a Name.");
        }

        if (NavigationHost.Count > 0 && hostName != null)
        {
            switch (hostName.Length)
            {
                case 0:
                    NavigationHost.First().Value.NavigateAndReset<IAmViewFor<T>>(contract, parameter);
                    break;
                default:
                    if (NavigationHost.TryGetValue(hostName, out var value))
                    {
                        value.NavigateAndReset<IAmViewFor<T>>(contract, parameter);
                    }

                    break;
            }
        }

        return @this;
    }

    /// <summary>
    /// Sets the main navigation host.
    /// </summary>
    /// <param name="this">The dummy.</param>
    /// <param name="viewHost">The view host.</param>
    public static void SetMainNavigationHost(this ISetNavigation @this, IViewRoutedViewModelHost viewHost)
    {
        if (@this == null)
        {
            throw new ArgumentNullException(nameof(@this));
        }

        if (viewHost == null)
        {
            throw new ArgumentNullException(nameof(viewHost));
        }

        if (NavigationHost.ContainsKey(@this.Name))
        {
            return;
        }

        WhenSetupSubjects.Add(@this.Name, new(1));
        NavigationHost.Add(@this.Name, viewHost);
        CurrentViewDisposable.Add(@this.Name, []);
        ResultNavigating.Add(@this.Name, new Subject<IViewNavigatingEventArgs>());

        if (viewHost.RequiresSetup)
        {
            viewHost.Setup();
        }

        ASetupCompleted.OnNext(Unit.Default);
        WhenSetupSubjects[@this.Name].OnNext(true);
    }

    /// <summary>
    /// Whens the navigated from.
    /// </summary>
    /// <param name="this">The this.</param>
    /// <param name="e">The e.</param>
    public static void WhenNavigatedFrom(this INotifiyNavigation @this, Action<IViewNavigationEventArgs> e)
    {
        if (@this == null)
        {
            throw new ArgumentNullException(nameof(@this));
        }

        @this.ISetupNavigatedFrom = true;
        var vm = (@this as IAmViewFor)?.ViewModel as INotifiyRoutableView;
        SetWhenNavigated.Where(x => x.From != null && x.From.Name == vm?.Name).Subscribe(ea =>
        {
            e(ea);
            ea.From?.WhenNavigatedFrom(ea);
        }).DisposeWith(@this.CleanUp);
    }

    /// <summary>
    /// Whens the navigated to.
    /// </summary>
    /// <param name="this">The this.</param>
    /// <param name="e">The e.</param>
    public static void WhenNavigatedTo(this INotifiyNavigation @this, Action<IViewNavigationEventArgs, CompositeDisposable> e)
    {
        if (@this == null)
        {
            throw new ArgumentNullException(nameof(@this));
        }

        @this.ISetupNavigatedTo = true;
        var vm = (@this as IAmViewFor)?.ViewModel as INotifiyRoutableView;
        SetWhenNavigated.Where(x => x?.To?.Name == vm?.Name).Subscribe(ea =>
        {
            if (ea.NavigationType == NavigationType.New)
            {
                CurrentViewDisposable[ea.HostName]?.Dispose();
                CurrentViewDisposable[ea.HostName] = [];
            }

            e(ea, CurrentViewDisposable[ea.HostName]);
            ea?.To?.WhenNavigatedTo(ea, CurrentViewDisposable[ea.HostName]);
        }).DisposeWith(@this.CleanUp);
    }

    /// <summary>
    /// Called when [navigating].
    /// </summary>
    /// <param name="this">The this.</param>
    /// <param name="e">
    /// The <see cref="IViewNavigatingEventArgs"/> instance containing the event data.
    /// </param>
    public static void WhenNavigating(this INotifiyNavigation @this, Func<IViewNavigatingEventArgs, IViewNavigatingEventArgs> e)
    {
        if (@this == null)
        {
            throw new ArgumentNullException(nameof(@this));
        }

        @this.ISetupNavigating = true;
        var vm = (@this as IAmViewFor)?.ViewModel as INotifiyRoutableView;
        SetWhenNavigating.Where(x => x?.From == null || x.From.Name == vm?.Name).Subscribe(ea =>
        {
            if (ea != null)
            {
                if (ea.From != null)
                {
                    e(ea);
                }

                ea.From?.WhenNavigating(ea);

                ResultNavigating[ea.HostName].OnNext(ea);
            }
        }).DisposeWith(@this.CleanUp);
    }

    /// <summary>
    /// Whens the activated.
    /// </summary>
    /// <param name="this">The this.</param>
    /// <returns>A Bool.</returns>
    public static IObservable<bool> WhenSetup(this IUseNavigation @this) =>
        Observable.Create<bool>(obs =>
            {
                var dis = new CompositeDisposable();
                ASetupCompleted.Subscribe(_ =>
                {
                    if (WhenSetupSubjects.Count > 0 && @this.Name != null)
                    {
                        switch (@this.Name.Length)
                        {
                            case 0:
                                {
                                    WhenSetupSubjects.First().Value.Where(x => x).Subscribe(obs).DisposeWith(dis);
                                    break;
                                }

                            default:
                                if (NavigationHost.ContainsKey(@this.Name))
                                {
                                    WhenSetupSubjects[@this.Name].Where(x => x).Subscribe(obs).DisposeWith(dis);
                                }

                                break;
                        }
                    }
                }).DisposeWith(dis);
                return dis;
            });

    /// <summary>
    /// Whens the activated.
    /// </summary>
    /// <param name="dummy">The dummy.</param>
    /// <param name="hostName">Name of the host.</param>
    /// <returns>
    /// A Bool.
    /// </returns>
    public static IObservable<bool> WhenSetup(this IUseHostedNavigation dummy, string? hostName = "") =>
        Observable.Create<bool>(obs =>
            {
                var dis = new CompositeDisposable();
                ASetupCompleted.Subscribe(_ =>
                {
                    if (WhenSetupSubjects.Count > 0)
                    {
                        if (hostName?.Length > 0)
                        {
                            if (NavigationHost.ContainsKey(hostName))
                            {
                                WhenSetupSubjects[hostName].Where(x => x).Subscribe(obs).DisposeWith(dis);
                            }
                        }
                        else
                        {
                            WhenSetupSubjects.First().Value.Where(x => x).Subscribe(obs).DisposeWith(dis);
                        }
                    }
                }).DisposeWith(dis);
                return dis;
            });
#pragma warning restore RCS1175 // Unused 'this' parameter.
}
