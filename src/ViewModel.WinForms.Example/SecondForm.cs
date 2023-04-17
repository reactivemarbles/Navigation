// Copyright (c) 2019-2023 ReactiveUI Association Incorporated. All rights reserved.
// ReactiveUI Association Incorporated licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using ReactiveMarbles.Command;
using ReactiveMarbles.ObservableEvents;
using ReactiveMarbles.ViewModel.Core;
using ReactiveMarbles.ViewModel.WinForms;

namespace ViewModel.WinForms.Example;

/// <summary>
/// SecondForm.
/// </summary>
/// <seealso cref="System.Windows.Forms.Form" />
public partial class SecondForm : NavigationForm
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SecondForm"/> class.
    /// </summary>
    public SecondForm()
    {
        InitializeComponent();
        this.Events().Load.Subscribe(_ =>
        {
            this.NavigateToView<FirstViewModel>();
            ////NavBack.Command = RxCommand.Create(() => this.NavigateBack(), CanNavigateBack);
        });
    }
}
