// Copyright (c) 2019-2023 ReactiveUI Association Incorporated. All rights reserved.
// ReactiveUI Association Incorporated licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using Foundation;

namespace ViewModel.MAUI.Example;

/// <summary>
/// AppDelegate.
/// </summary>
[Register("AppDelegate")]
public class AppDelegate : MauiUIApplicationDelegate
{
    /// <summary>
    /// Creates the maui application.
    /// </summary>
    /// <returns>A MauiApp.</returns>
    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
}
