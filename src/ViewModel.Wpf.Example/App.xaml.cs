// Copyright (c) 2019-2023 ReactiveUI Association Incorporated. All rights reserved.
// ReactiveUI Association Incorporated licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System.Reactive.Concurrency;
using System.Windows;
using ReactiveMarbles.Locator;
using ReactiveMarbles.Mvvm;

namespace ViewModel.Wpf.Example
{
    /// <summary>
    /// Interaction logic for App.xaml.
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="App"/> class.
        /// </summary>
        public App()
        {
            ServiceLocator.Current().AddCoreRegistrations(() =>
            CoreRegistrationBuilder
                .Create()
                .WithMainThreadScheduler(DefaultScheduler.Instance)
                .WithTaskPoolScheduler(TaskPoolScheduler.Default)
                .WithExceptionHandler(new DebugExceptionHandler())
                .Build());
            ServiceLocator.Current().AddSingleton<MainWindowViewModel>(() => new());
        }
    }
}
