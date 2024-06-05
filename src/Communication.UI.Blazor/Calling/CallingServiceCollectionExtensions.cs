//-----------------------------------------------------------------------
// <copyright file="CallingServiceCollectionExtensions.cs" company="P.O.S Informatique">
//     Copyright (c) P.O.S Informatique. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Microsoft.Extensions.DependencyInjection
{
    using PosInformatique.Azure.Communication.UI.Blazor;

    /// <summary>
    /// Contains extensions method to register Azure Communication Services UI Library services.
    /// </summary>
    public static class CallingServiceCollectionExtensions
    {
        /// <summary>
        /// Add Azure Communication Services services used for the calling operation.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/> where the services will be registered.</param>
        /// <returns>The <paramref name="services"/> instance to continue to register additionnal services.</returns>
        public static IServiceCollection AddCalling(this IServiceCollection services)
        {
            return services.AddSingleton<ICallingService, CallingService>();
        }
    }
}
