// Copyright (c) 2019-2023 ReactiveUI Association Incorporated. All rights reserved.
// ReactiveUI Association Incorporated licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System.ComponentModel;
using ReactiveMarbles.ViewModel.Core;

namespace ReactiveMarbles.ViewModel.WinForms
{
    /// <summary>
    /// NavigationForm.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class NavigationForm : Form, ISetNavigation, IUseNavigation
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NavigationForm"/> class.
        /// </summary>
        public NavigationForm() => InitializeComponent();

        /// <summary>
        /// Gets or sets a value indicating whether [navigate back is enabled].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [navigate back is enabled]; otherwise, <c>false</c>.
        /// </value>
        [Category("ReactiveMarbles")]
        [Description("A value indicating if Navigating back is enabled.")]
        [Bindable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool NavigateBackIsEnabled { get; set; }

        /// <summary>
        /// Gets the can navigate back.
        /// </summary>
        /// <value>
        /// The can navigate back.
        /// </value>
        public IObservable<bool> CanNavigateBack => NavigationFrame.CanNavigateBackObservable;

        /// <summary>
        /// Gets the navigation frame.
        /// </summary>
        /// <value>
        /// The navigation frame.
        /// </value>
        public ViewModelRoutedViewHost NavigationFrame { get; } = new();

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Form.Load" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnLoad(EventArgs e)
        {
            NavigationFrame.HostName = Name;
            this.SetMainNavigationHost(NavigationFrame);
            base.OnLoad(e);
        }
    }
}
