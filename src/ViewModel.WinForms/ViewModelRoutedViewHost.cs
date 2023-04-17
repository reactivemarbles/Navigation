// Copyright (c) 2019-2023 ReactiveUI Association Incorporated. All rights reserved.
// ReactiveUI Association Incorporated licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System.Collections.ObjectModel;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using ReactiveMarbles.Locator;
using ReactiveMarbles.ViewModel.Core;

namespace ReactiveMarbles.ViewModel.WinForms
{
    /// <summary>
    /// ViewModelRoutedViewHost.
    /// </summary>
    /// <seealso cref="UserControl" />
    /// <seealso cref="IViewModelRoutedViewHost" />
    public partial class ViewModelRoutedViewHost : UserControl, IViewModelRoutedViewHost
    {
        private readonly ISubject<bool> _canNavigateBackSubject = new Subject<bool>();
        private readonly ISubject<INotifiyRoutableViewModel> _currentViewModelSubject = new Subject<INotifiyRoutableViewModel>();
        private IRxObject? _currentViewModel;
        private IAmViewFor? _currentView;
        private IAmViewFor? _lastView;
        private bool _navigateBack;
        private bool _resetStack;
        private IRxObject? _toViewModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewModelRoutedViewHost"/> class.
        /// </summary>
        public ViewModelRoutedViewHost() => InitializeComponent();

        /// <summary>
        /// Gets the navigation stack.
        /// </summary>
        /// <value>
        /// The navigation stack.
        /// </value>
        public ObservableCollection<IRxObject?> NavigationStack { get; } = new();

        /// <summary>
        /// Gets the current view model.
        /// </summary>
        /// <value>
        /// The current view model.
        /// </value>
        public IObservable<INotifiyRoutableViewModel> CurrentViewModel => _currentViewModelSubject.Publish().RefCount();

        /// <summary>
        /// Gets or sets a value indicating whether [navigate back is enabled].
        /// </summary>
        /// <value>
        /// <c>true</c> if [navigate back is enabled]; otherwise, <c>false</c>.
        /// </value>
        public bool CanNavigateBack { get; set; }

        /// <summary>
        /// Gets the can navigate back observable.
        /// </summary>
        /// <value>
        /// The can navigate back observable.
        /// </value>
        public IObservable<bool> CanNavigateBackObservable => _canNavigateBackSubject;

        /// <summary>
        /// Gets or sets the name of the host.
        /// </summary>
        /// <value>
        /// The name of the host.
        /// </value>
        public string HostName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a value indicating whether [navigate back is enabled].
        /// </summary>
        /// <value>
        /// <c>true</c> if [navigate back is enabled]; otherwise, <c>false</c>.
        /// </value>
        public bool NavigateBackIsEnabled { get; set; }

        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        /// <value>
        /// The content.
        /// </value>
        public object? Content { get; set; }

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
            where T : class, IRxObject => InternalNavigate<T>(contract, parameter);

        /// <summary>
        /// Navigates the ViewModel contract.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <param name="parameter">The parameter.</param>
        public void Navigate(IRxObject viewModel, object? parameter = null)
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
            where T : class, IRxObject
        {
            _resetStack = true;
            InternalNavigate<T>(contract, parameter);
        }

        /// <summary>
        /// Navigates the and reset.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <param name="parameter">The parameter.</param>
        public void NavigateAndReset(IRxObject viewModel, object? parameter = null)
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

                if (_currentView is INotifiyNavigation { ISetupNavigating: true })
                {
                    ViewModelRoutedViewHostMixins.SetWhenNavigating.OnNext(new ViewModelNavigatingEventArgs(_currentViewModel, _toViewModel, NavigationType.Back, _lastView, HostName, parameter));
                }
                else
                {
                    ViewModelRoutedViewHostMixins.ResultNavigating[HostName].OnNext(new ViewModelNavigatingEventArgs(_currentViewModel, _toViewModel, NavigationType.Back, _lastView, HostName, parameter));
                }
            }

            CanNavigateBack = NavigationStack.Count > 1;
            _canNavigateBackSubject.OnNext(CanNavigateBack);
        }

        /// <summary>
        /// Refreshes this instance.
        /// </summary>
        void IViewModelRoutedViewHost.Refresh()
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
        /// <exception cref="ArgumentNullException">Navigation Host Name not set.</exception>
        public void Setup()
        {
            if (string.IsNullOrWhiteSpace(HostName))
            {
                throw new ArgumentNullException(HostName, "Navigation Host Name not set");
            }

            // requested should return result here
            ViewModelRoutedViewHostMixins.ResultNavigating[HostName].DistinctUntilChanged().ObserveOn(DispatcherScheduler.Current).Subscribe(e =>
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
                    _toViewModel ??= e.View?.ViewModel as IRxObject;
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
            where T : class, IRxObject
        {
            _toViewModel = ServiceLocator.Current().GetServiceWithContract<T>(contract);
            _lastView = _currentView;

            // NOTE: This gets a new instance of the View
            _currentView = ServiceLocator.Current().GetView<T>(contract);

            if (_currentView is INotifiyNavigation { ISetupNavigating: true })
            {
                ViewModelRoutedViewHostMixins.SetWhenNavigating.OnNext(new ViewModelNavigatingEventArgs(_currentViewModel, _toViewModel, NavigationType.New, _currentView, HostName, parameter));
            }
            else
            {
                var ea = new ViewModelNavigatingEventArgs(_currentViewModel, _toViewModel, NavigationType.New, _currentView, HostName, parameter);
                ViewModelRoutedViewHostMixins.ResultNavigating[HostName].OnNext(ea);
            }
        }

        private void InternalNavigate(IRxObject viewModel, object? parameter)
        {
            _toViewModel = viewModel;
            _lastView = _currentView;

            // NOTE: This gets a new instance of the View
            _currentView = ServiceLocator.Current().GetView(viewModel);

            if (_currentView is INotifiyNavigation { ISetupNavigating: true })
            {
                ViewModelRoutedViewHostMixins.SetWhenNavigating.OnNext(new ViewModelNavigatingEventArgs(_currentViewModel, _toViewModel, NavigationType.New, _currentView, HostName, parameter));
            }
            else
            {
                var ea = new ViewModelNavigatingEventArgs(_currentViewModel, _toViewModel, NavigationType.New, _currentView, HostName, parameter);
                ViewModelRoutedViewHostMixins.ResultNavigating[HostName].OnNext(ea);
            }
        }
    }
}
