//-----------------------------------------------------------------------
// <copyright file="RemoteParticipantJoinedEvent.cs" company="P.O.S Informatique">
//     Copyright (c) P.O.S Informatique. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace PosInformatique.Azure.Communication.UI.Blazor
{
    /// <summary>
    /// Event occured when a <see cref="RemoteParticipant"/> joined the call.
    /// </summary>
    public class RemoteParticipantJoinedEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RemoteParticipantJoinedEvent"/> class.
        /// </summary>
        /// <param name="participant"><see cref="RemoteParticipant"/> who joined the call.</param>
        public RemoteParticipantJoinedEvent(RemoteParticipant participant)
        {
            this.Participant = participant;
        }

        /// <summary>
        /// Gets the <see cref="RemoteParticipant"/> who join the call.
        /// </summary>
        public RemoteParticipant Participant { get; }
    }
}
