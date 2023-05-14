// Copyright (c) 2019-2023 ReactiveUI Association Incorporated. All rights reserved.
// ReactiveUI Association Incorporated licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using ReactiveMarbles.ViewModel.Core;

namespace ReactiveMarbles.Locator;

/// <summary>
/// ServiceLocatorMixins.
/// </summary>
public static class ServiceLocatorMixins
{
    /// <summary>
    /// Adds the navigation view.
    /// </summary>
    /// <typeparam name="TView">The type of the view.</typeparam>
    /// <typeparam name="TViewModel">The type of the view model.</typeparam>
    /// <param name="serviceLocator">The service locator.</param>
    public static void AddNavigationView<TView, TViewModel>(this IServiceLocator serviceLocator)
       where TView : class, IAmViewFor<TViewModel>, new()
       where TViewModel : class, IRxNavBase
    {
        if (serviceLocator == null)
        {
            throw new ArgumentNullException(nameof(serviceLocator));
        }

        serviceLocator.AddService<object>(() => new TView(), typeof(TViewModel).FullName!);
        serviceLocator.AddService<IAmViewFor<TViewModel>>(() => new TView());
        serviceLocator.AddService(() => ServiceLocator.Current().GetService<IAmViewFor<TViewModel>>() as TView);
    }

    /// <summary>
    /// Adds the navigation view.
    /// </summary>
    /// <typeparam name="TView">The type of the view.</typeparam>
    /// <typeparam name="TViewModel">The type of the view model.</typeparam>
    /// <param name="serviceLocator">The service locator.</param>
    /// <param name="contract">The contract.</param>
    /// <exception cref="System.ArgumentNullException">serviceLocator.</exception>
    public static void AddNavigationView<TView, TViewModel>(this IServiceLocator serviceLocator, string contract)
       where TView : class, IAmViewFor<TViewModel>, new()
       where TViewModel : class, IRxNavBase
    {
        if (serviceLocator == null)
        {
            throw new ArgumentNullException(nameof(serviceLocator));
        }

        serviceLocator.AddService<object>(() => new TView(), typeof(TViewModel).FullName!);
        serviceLocator.AddService<IAmViewFor<TViewModel>>(() => new TView(), contract);
        serviceLocator.AddService(() => ServiceLocator.Current().GetService<IAmViewFor<TViewModel>>() as TView, contract);
    }

    /// <summary>
    /// Gets the service.
    /// </summary>
    /// <typeparam name="T">The Type.</typeparam>
    /// <param name="serviceLocator">The service locator.</param>
    /// <param name="contract">The contract.</param>
    /// <returns>
    /// An instance.
    /// </returns>
    /// <exception cref="System.ArgumentNullException">serviceLocator.</exception>
    public static IAmViewFor? GetView<T>(this IServiceLocator serviceLocator, string? contract = null)
        where T : class, IRxNavBase
    {
        if (serviceLocator == null)
        {
            throw new ArgumentNullException(nameof(serviceLocator));
        }

        return serviceLocator.GetServiceWithContract<IAmViewFor<T>>(contract);
    }

    /// <summary>
    /// Gets the service.
    /// </summary>
    /// <param name="serviceLocator">The service locator.</param>
    /// <param name="viewModel">The view model.</param>
    /// <returns>
    /// An instance.
    /// </returns>
    /// <exception cref="System.ArgumentNullException">serviceLocator.</exception>
    public static IAmViewFor? GetView(this IServiceLocator serviceLocator, IRxNavBase viewModel)
    {
        if (serviceLocator == null)
        {
            throw new ArgumentNullException(nameof(serviceLocator));
        }

        if (viewModel == null)
        {
            throw new ArgumentNullException(nameof(viewModel));
        }

        return (IAmViewFor)serviceLocator.GetServiceWithContract<object>(viewModel.GetType().FullName);
    }

    /// <summary>
    /// Gets the service.
    /// </summary>
    /// <typeparam name="T">The type.</typeparam>
    /// <param name="serviceLocator">The service locator.</param>
    /// <param name="contract">The contract.</param>
    /// <returns>An instance of T.</returns>
    /// <exception cref="System.ArgumentNullException">serviceLocator.</exception>
    public static T GetServiceWithContract<T>(this IServiceLocator serviceLocator, string? contract = null)
    {
        if (serviceLocator == null)
        {
            throw new ArgumentNullException(nameof(serviceLocator));
        }

        if (string.IsNullOrWhiteSpace(contract))
        {
            return serviceLocator.GetService<T>();
        }

        return serviceLocator.GetService<T>(contract!);
    }
}
