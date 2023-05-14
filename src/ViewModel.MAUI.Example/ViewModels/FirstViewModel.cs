// Copyright (c) 2019-2023 ReactiveUI Association Incorporated. All rights reserved.
// ReactiveUI Association Incorporated licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System.Diagnostics;
using System.Reactive;
using System.Reactive.Disposables;
using ReactiveMarbles.Command;
using ReactiveMarbles.ViewModel.Core;

namespace ViewModel.MAUI.Example
{
    /// <summary>
    /// FirstViewModel.
    /// </summary>
    public class FirstViewModel : RxNavBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FirstViewModel"/> class.
        /// </summary>
        public FirstViewModel()
        {
            GotoMain = RxCommand.Create(() => this.NavigateToView<MainViewModel>());

            GotoFirst = RxCommand.Create(() => this.NavigateBack(), this.CanNavigateBack());
        }

        /// <summary>
        /// Gets the goto main.
        /// </summary>
        /// <value>
        /// The goto main.
        /// </value>
        public RxCommand<Unit, IUseHostedNavigation> GotoMain { get; }

        /// <summary>
        /// Gets the goto first.
        /// </summary>
        /// <value>
        /// The goto first.
        /// </value>
        public RxCommand<Unit, IUseHostedNavigation> GotoFirst { get; }

        /// <summary>
        /// WhenNavigatedTo.
        /// </summary>
        /// <param name="e"></param>
        /// <param name="disposables"></param>
        /// <inheritdoc />
        public override void WhenNavigatedTo(IViewModelNavigationEventArgs e, CompositeDisposable disposables)
        {
            if (e is null)
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
            if (e is null)
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
            if (e is null)
            {
                throw new ArgumentNullException(nameof(e));
            }

            Debug.WriteLine($"{DateTime.Now.ToString()} Navigating From: {e.From?.Name} To: {e.To?.Name} with Host {e.HostName}");
            base.WhenNavigating(e);
        }
    }
}
