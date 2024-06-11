//-----------------------------------------------------------------------
// <copyright file="RemoteParticipantLeftEvent.cs" company="P.O.S Informatique">
//     Copyright (c) P.O.S Informatique. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace PosInformatique.Azure.Communication.UI.Blazor
{
    /// <summary>
    /// Event occured when a <see cref="RemoteParticipant"/> left the call.
    /// </summary>
    public class RemoteParticipantLeftEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RemoteParticipantLeftEvent"/> class.
        /// </summary>
        /// <param name="participant"><see cref="RemoteParticipant"/> who left the call.</param>
        public RemoteParticipantLeftEvent(RemoteParticipant participant)
        {
            this.Participant = participant;
        }

        /// <summary>
        /// Gets the <see cref="RemoteParticipant"/> who left the call.
        /// </summary>
        public RemoteParticipant Participant { get; }
    }
}
