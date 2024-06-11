//-----------------------------------------------------------------------
// <copyright file="MicrophoneMuteChangedEvent.cs" company="P.O.S Informatique">
//     Copyright (c) P.O.S Informatique. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace PosInformatique.Azure.Communication.UI.Blazor
{
    /// <summary>
    /// Event occured when the microphone is muted or unmuted on a participant.
    /// </summary>
    public class MicrophoneMuteChangedEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MicrophoneMuteChangedEvent"/> class.
        /// </summary>
        /// <param name="participantId">Participant identifier which the microphone has been mute/unmute.</param>
        /// <param name="isMuted">Indicating whether if the microphone is muted.</param>
        public MicrophoneMuteChangedEvent(CommunicationUserKind participantId, bool isMuted)
        {
            this.ParticipantId = participantId;
            this.IsMuted = isMuted;
        }

        /// <summary>
        /// Gets the participant identifier which the microphone has been mute/unmute.
        /// </summary>
        public CommunicationUserKind ParticipantId { get; }

        /// <summary>
        /// Gets a value indicating whether of the microphone is muted.
        /// </summary>
        public bool IsMuted { get; }
    }
}
