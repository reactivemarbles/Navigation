// Copyright (c) 2019-2023 ReactiveUI Association Incorporated. All rights reserved.
// ReactiveUI Association Incorporated licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Windows;
using System.Windows.Controls;
using ReactiveMarbles.Locator;
using ReactiveMarbles.Mvvm;
using ReactiveMarbles.ViewModel.Core;

namespace ReactiveMarbles.ViewModel.Wpf;

/// <summary>
/// View Model Routed View Host.
/// </summary>
public class ViewModelRoutedViewHost : ContentControl, IViewModelRoutedViewHost
{
    /// <summary>
    /// The navigate back is enabled property.
    /// </summary>
    public static readonly DependencyProperty CanNavigateBackProperty = DependencyProperty.Register(nameof(CanNavigateBack), typeof(bool), typeof(ViewModelRoutedViewHost), new PropertyMetadata(false));

    /// <summary>
    /// The host name property.
    /// </summary>
    public static readonly DependencyProperty HostNameProperty = DependencyProperty.Register(nameof(HostName), typeof(string), typeof(ViewModelRoutedViewHost), new PropertyMetadata(string.Empty));

    /// <summary>
    /// The navigate back is enabled property.
    /// </summary>
    public static readonly DependencyProperty NavigateBackIsEnabledProperty = DependencyProperty.Register(nameof(NavigateBackIsEnabled), typeof(bool), typeof(ViewModelRoutedViewHost), new PropertyMetadata(true));

    private readonly ISubject<bool> _canNavigateBackSubject = new Subject<bool>();
    private readonly ISubject<INotifiyRoutableViewModel> _currentViewModelSubject = new Subject<INotifiyRoutableViewModel>();
    private IRxNavBase? _currentViewModel;
    private IAmViewFor? _currentView;
    private IAmViewFor? _lastView;
    private bool _navigateBack;
    private bool _resetStack;
    private IRxNavBase? _toViewModel;
    private ICoreRegistration? _coreRegistration;

    /// <summary>
    /// Initializes a new instance of the <see cref="ViewModelRoutedViewHost"/> class.
    /// </summary>
    public ViewModelRoutedViewHost() =>
        CurrentViewModel.Subscribe(vm =>
            {
                if (vm is IRxNavBase && !_navigateBack)
                {
                    _currentViewModel = vm as IRxNavBase;
                    NavigationStack.Add(_currentViewModel);
                }

                if (_currentView != null)
                {
                    Content = _currentView;
                }

                CanNavigateBack = NavigationStack?.Count > 1;
                _canNavigateBackSubject.OnNext(CanNavigateBack);
            });

    /// <summary>
    /// Gets or sets a value indicating whether [navigate back is enabled].
    /// </summary>
    /// <value><c>true</c> if [navigate back is enabled]; otherwise, <c>false</c>.</value>
    public bool CanNavigateBack
    {
        get => (bool)GetValue(CanNavigateBackProperty);
        set => SetValue(CanNavigateBackProperty, value);
    }

    /// <summary>
    /// Gets the can navigate back observable.
    /// </summary>
    /// <value>
    /// The can navigate back observable.
    /// </value>
    public IObservable<bool> CanNavigateBackObservable => _canNavigateBackSubject;

    /// <summary>
    /// Gets the current view model.
    /// </summary>
    /// <value>
    /// The current view model.
    /// </value>
    public IObservable<INotifiyRoutableViewModel> CurrentViewModel => _currentViewModelSubject.Publish().RefCount();

    /// <summary>
    /// Gets or sets the name of the host.
    /// </summary>
    /// <value>
    /// The name of the host.
    /// </value>
    public string HostName
    {
        get => (string)GetValue(HostNameProperty);
        set => SetValue(HostNameProperty, value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether [navigate back is enabled].
    /// </summary>
    /// <value>
    ///   <c>true</c> if [navigate back is enabled]; otherwise, <c>false</c>.
    /// </value>
    public bool NavigateBackIsEnabled
    {
        get => (bool)GetValue(NavigateBackIsEnabledProperty);
        set => SetValue(NavigateBackIsEnabledProperty, value);
    }

    /// <summary>
    /// Gets the navigation stack.
    /// </summary>
    /// <value>
    /// The navigation stack.
    /// </value>
    public ObservableCollection<IRxNavBase?> NavigationStack { get; } = new();

    /// <summary>
    /// Gets a value indicating whether [requires setup].
    /// </summary>
    /// <value>
    /// <c>true</c> if [requires setup]; otherwise, <c>false</c>.
    /// </value>
    public bool RequiresSetup => false;

    /// <summary>
    /// Clears the history.
    /// </summary>
    public void ClearHistory() => NavigationStack.Clear();

    /// <summary>
    /// Navigates the ViewModel contract.
    /// </summary>
    /// <typeparam name="T">The Type.</typeparam>
    /// <param name="contract">The contract.</param>
    /// <param name="parameter">The parameter.</param>
    public void Navigate<T>(string? contract = null, object? parameter = null)
        where T : class, IRxNavBase => InternalNavigate<T>(contract, parameter);

    /// <summary>
    /// Navigates the ViewModel contract.
    /// </summary>
    /// <param name="viewModel">The view model.</param>
    /// <param name="parameter">The parameter.</param>
    public void Navigate(IRxNavBase viewModel, object? parameter = null)
    {
        if (viewModel is null)
        {
            throw new ArgumentNullException(nameof(viewModel));
        }

        InternalNavigate(viewModel, parameter);
    }

    /// <summary>
    /// Navigates and resets.
    /// </summary>
    /// <typeparam name="T">The Type.</typeparam>
    /// <param name="contract">The contract.</param>
    /// <param name="parameter">The parameter.</param>
    public void NavigateAndReset<T>(string? contract = null, object? parameter = null)
        where T : class, IRxNavBase
    {
        _resetStack = true;
        InternalNavigate<T>(contract, parameter);
    }

    /// <summary>
    /// Navigates the and reset.
    /// </summary>
    /// <param name="viewModel">The view model.</param>
    /// <param name="parameter">The parameter.</param>
    public void NavigateAndReset(IRxNavBase viewModel, object? parameter = null)
    {
        if (viewModel is null)
        {
            throw new ArgumentNullException(nameof(viewModel));
        }

        _resetStack = true;
        InternalNavigate(viewModel, parameter);
    }

    /// <summary>
    /// Navigates back.
    /// </summary>
    /// <param name="parameter">The parameter.</param>
    public void NavigateBack(object? parameter = null)
    {
        if (NavigateBackIsEnabled && CanNavigateBack && NavigationStack.Count > 1)
        {
            _navigateBack = true;

            // Get the previous View
            var count = NavigationStack.Count - 2;

            _toViewModel = NavigationStack[count];

            var ea = new ViewModelNavigatingEventArgs(_currentViewModel, _toViewModel, NavigationType.Back, _lastView, HostName, parameter);
            if (_currentView is INotifiyNavigation { ISetupNavigating: true })
            {
                ViewModelRoutedViewHostMixins.SetWhenNavigating.OnNext(ea);
            }
            else
            {
                ViewModelRoutedViewHostMixins.ResultNavigating[HostName].OnNext(ea);
            }
        }

        CanNavigateBack = NavigationStack.Count > 1;
        _canNavigateBackSubject.OnNext(CanNavigateBack);
    }

    /// <inheritdoc />
    public override void OnApplyTemplate()
    {
        Setup();
        base.OnApplyTemplate();
    }

    /// <summary>
    /// Refreshes this instance.
    /// </summary>
    public void Refresh()
    {
        // Keep existing view
        if (Content == null && _currentView != null)
        {
            Content = _currentView;
        }

        if (!NavigateBackIsEnabled)
        {
            // cleanup while Navigation Back is disabled
            while (NavigationStack.Count > 1)
            {
                NavigationStack.RemoveAt(0);
            }
        }
    }

    /// <summary>
    /// Setups this instance.
    /// </summary>
    /// <exception cref="System.ArgumentNullException">Navigation Host Name not set.</exception>
    public void Setup()
    {
        if (string.IsNullOrWhiteSpace(HostName))
        {
            throw new ArgumentNullException(HostName, "Navigation Host Name not set");
        }

        _coreRegistration = ServiceLocator.Current().GetService<ICoreRegistration>();

        // requested should return result here
        ViewModelRoutedViewHostMixins.ResultNavigating[HostName].DistinctUntilChanged().ObserveOn(_coreRegistration.MainThreadScheduler).Subscribe(e =>
        {
            var fromView = _currentView as INotifiyNavigation;
            if (fromView?.ISetupNavigating == false || fromView?.ISetupNavigating == null)
            {
                // No view is setup for reciving navigation notifications.
                _currentViewModel?.WhenNavigating(e);
            }

            if (!e.Cancel)
            {
                var nea = new ViewModelNavigationEventArgs(_currentViewModel, _toViewModel, _navigateBack ? NavigationType.Back : NavigationType.New, e.View, HostName, e.NavigationParameter);
                var toView = e.View as INotifiyNavigation;
                var callVmNavTo = toView == null || !toView!.ISetupNavigatedTo;
                var callVmNavFrom = fromView == null || !fromView!.ISetupNavigatedTo;
                var cvm = _currentViewModel;
                _toViewModel ??= e.View?.ViewModel as IRxNavBase;
                var tvm = _toViewModel;

                if (_navigateBack)
                {
                    // Remove the current vm from the stack
                    NavigationStack.RemoveAt(NavigationStack.Count - 1);
                    if (tvm != null)
                    {
                        _currentView = ServiceLocator.Current().GetView(tvm);
                        _currentViewModelSubject.OnNext(tvm);
                        foreach (var host in ViewModelRoutedViewHostMixins.NavigationHost.Where(x => x.Key != HostName).Select(x => x.Key))
                        {
                            ViewModelRoutedViewHostMixins.NavigationHost[host].Refresh();
                        }
                    }
                }
                else if (tvm != null && _resetStack)
                {
                    NavigationStack.Clear();
                    _currentViewModelSubject.OnNext(tvm);
                }
                else if (tvm != null && _currentView != null)
                {
                    _currentViewModelSubject.OnNext(tvm);
                }

                if (toView?.ISetupNavigatedTo == true || fromView?.ISetupNavigatedFrom == true)
                {
                    ViewModelRoutedViewHostMixins.SetWhenNavigated.OnNext(nea);
                }

                if (callVmNavTo)
                {
                    tvm?.WhenNavigatedTo(nea, ViewModelRoutedViewHostMixins.CurrentViewDisposable[HostName]);
                }

                if (callVmNavFrom)
                {
                    cvm?.WhenNavigatedFrom(nea);
                }
            }

            CanNavigateBack = NavigationStack?.Count > 1;
            _canNavigateBackSubject.OnNext(CanNavigateBack);
            _resetStack = false;
            _navigateBack = false;
        });
    }

    private void InternalNavigate<T>(string? contract, object? parameter)
        where T : class, IRxNavBase
    {
        _toViewModel = ServiceLocator.Current().GetServiceWithContract<T>(contract);
        _lastView = _currentView;

        // NOTE: This gets a new instance of the View
        _currentView = ServiceLocator.Current().GetView<T>(contract);

        var ea = new ViewModelNavigatingEventArgs(_currentViewModel, _toViewModel, NavigationType.New, _currentView, HostName, parameter);
        if (_currentView is INotifiyNavigation { ISetupNavigating: true })
        {
            ViewModelRoutedViewHostMixins.SetWhenNavigating.OnNext(ea);
        }
        else
        {
            ViewModelRoutedViewHostMixins.ResultNavigating[HostName].OnNext(ea);
        }
    }

    private void InternalNavigate(IRxNavBase viewModel, object? parameter)
    {
        _toViewModel = viewModel;
        _lastView = _currentView;

        // NOTE: This gets a new instance of the View
        _currentView = ServiceLocator.Current().GetView(viewModel);

        var ea = new ViewModelNavigatingEventArgs(_currentViewModel, _toViewModel, NavigationType.New, _currentView, HostName, parameter);
        if (_currentView is INotifiyNavigation { ISetupNavigating: true })
        {
            ViewModelRoutedViewHostMixins.SetWhenNavigating.OnNext(ea);
        }
        else
        {
            ViewModelRoutedViewHostMixins.ResultNavigating[HostName].OnNext(ea);
        }
    }
}
