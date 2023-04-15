// Copyright (c) 2019-2023 ReactiveUI Association Incorporated. All rights reserved.
// ReactiveUI Association Incorporated licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System.Reactive.Disposables;

namespace ReactiveMarbles.ViewModel.Core;

/// <summary>
/// interface for RxBase.
/// </summary>
/// <seealso cref="System.IDisposable"/>
public interface IRxObject : INotifiyRoutableViewModel, ICancelable, IAmBuilt
{
}
