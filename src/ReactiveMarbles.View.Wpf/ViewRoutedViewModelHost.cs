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
using ReactiveMarbles.View.Core;
using ReactiveMarbles.ViewModel.Core;

namespace ReactiveMarbles.View.Wpf
{
    /// <summary>
    /// View Routed ViewModel Host.
    /// </summary>
    /// <seealso cref="ContentControl" />
    /// <seealso cref="IViewRoutedViewModelHost" />
    public class ViewRoutedViewModelHost : ContentControl, IViewRoutedViewModelHost
    {
        /// <summary>
        /// The navigate back is enabled property.
        /// </summary>
        public static readonly DependencyProperty CanNavigateBackProperty = DependencyProperty.Register(nameof(CanNavigateBack), typeof(bool), typeof(ViewRoutedViewModelHost), new PropertyMetadata(false));

        /// <summary>
        /// The host name property.
        /// </summary>
        public static readonly DependencyProperty HostNameProperty = DependencyProperty.Register(nameof(HostName), typeof(string), typeof(ViewRoutedViewModelHost), new PropertyMetadata(string.Empty));

        /// <summary>
        /// The navigate back is enabled property.
        /// </summary>
        public static readonly DependencyProperty NavigateBackIsEnabledProperty = DependencyProperty.Register(nameof(NavigateBackIsEnabled), typeof(bool), typeof(ViewRoutedViewModelHost), new PropertyMetadata(true));

        private readonly ISubject<bool> _canNavigateBackSubject = new Subject<bool>();
        private readonly ISubject<INotifiyRoutableView> _currentViewModelSubject = new Subject<INotifiyRoutableView>();
        private IRxNavBase? _currentViewModel;
        private IAmViewFor? _currentView;
        private IAmViewFor? _lastView;
        private bool _navigateBack;
        private bool _resetStack;
        private IRxNavBase? _toViewModel;
        private ICoreRegistration? _coreRegistration;

        /// <summary>
        /// Gets the navigation stack.
        /// </summary>
        /// <value>
        /// The navigation stack.
        /// </value>
        public ObservableCollection<IAmViewFor?> NavigationStack { get; } = [];

        /// <summary>
        /// Gets the current view model.
        /// </summary>
        /// <value>
        /// The current view model.
        /// </value>
        public IObservable<INotifiyRoutableView> CurrentView => _currentViewModelSubject;

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
        /// Gets a value indicating whether [requires setup].
        /// </summary>
        /// <value>
        /// <c>true</c> if [requires setup]; otherwise, <c>false</c>.
        /// </value>
        public bool RequiresSetup => false;

        /// <summary>
        /// Clears the history.
        /// </summary>
        public void ClearHistory() => throw new NotImplementedException();
        /// <summary>
        /// Navigates the specified contract.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <param name="parameter">The parameter.</param>
        public void Navigate(IAmViewFor viewModel, object? parameter = null) => throw new NotImplementedException();
        /// <summary>
        /// Navigates the and reset.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <param name="parameter">The parameter.</param>
        public void NavigateAndReset(IAmViewFor viewModel, object? parameter = null) => throw new NotImplementedException();
        /// <summary>
        /// Navigates the back.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public void NavigateBack(object? parameter = null) => throw new NotImplementedException();
        /// <summary>
        /// Refreshes this instance.
        /// </summary>
        public void Refresh() => throw new NotImplementedException();
        /// <summary>
        /// Setups this instance.
        /// </summary>
        public void Setup() => throw new NotImplementedException();
        /// <summary>
        /// Navigates the specified contract.
        /// </summary>
        /// <typeparam name="T">The Type.</typeparam>
        /// <param name="contract">The contract.</param>
        /// <param name="parameter">The parameter.</param>
        void IViewRoutedViewModelHost.Navigate<T>(string? contract, object? parameter) => throw new NotImplementedException();
        /// <summary>
        /// Navigates the and reset.
        /// </summary>
        /// <typeparam name="T">The Type.</typeparam>
        /// <param name="contract">The contract.</param>
        /// <param name="parameter">The parameter.</param>
        void IViewRoutedViewModelHost.NavigateAndReset<T>(string? contract, object? parameter) => throw new NotImplementedException();
    }
}
