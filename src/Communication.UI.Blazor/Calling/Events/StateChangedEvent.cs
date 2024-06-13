//-----------------------------------------------------------------------
// <copyright file="StateChangedEvent.cs" company="P.O.S Informatique">
//     Copyright (c) P.O.S Informatique. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace PosInformatique.Azure.Communication.UI.Blazor
{
    /// <summary>
    /// Contains information when the state of the <see cref="CallAdapter"/> has been changed.
    /// </summary>
    public class StateChangedEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StateChangedEvent"/> class.
        /// </summary>
        /// <param name="state">New state of the <see cref="CallAdapter"/>.</param>
        public StateChangedEvent(CallAdapterState state)
        {
            this.State = state;
        }

        /// <summary>
        /// Gets the new state of the <see cref="CallAdapter"/>.
        /// </summary>
        public CallAdapterState State { get; }
    }
}
