//-----------------------------------------------------------------------
// <copyright file="ICallingService.cs" company="P.O.S Informatique">
//     Copyright (c) P.O.S Informatique. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace PosInformatique.Azure.Communication.UI.Blazor
{
    /// <summary>
    /// Service of the Calling Azure Communication Services feature
    /// which allows to create <see cref="CallAdapter"/> instances.
    /// </summary>
    public interface ICallingService
    {
        /// <summary>
        /// Create a <see cref="CallAdapter"/> backed by Azure Communication Services.
        /// </summary>
        /// <param name="args">Parameters of the <see cref="CallAdapter"/> to create.</param>
        /// <returns>A new instance of the <see cref="CallAdapter"/> which can be use by a <see cref="CallComposite"/> using the <see cref="CallComposite.Adapter"/>
        /// property.</returns>
        Task<CallAdapter> CreateAdapterAsync(CallAdapterArgs args);
    }
}
