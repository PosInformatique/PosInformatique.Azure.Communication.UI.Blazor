//-----------------------------------------------------------------------
// <copyright file="RemoteParticipant.cs" company="P.O.S Informatique">
//     Copyright (c) P.O.S Informatique. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace PosInformatique.Azure.Communication.UI.Blazor
{
    /// <summary>
    /// Represents a remote participant in a call.
    /// </summary>
    public class RemoteParticipant : ParticipantInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RemoteParticipant"/> class.
        /// </summary>
        /// <param name="identifier">The identifier for this remote participant.</param>
        /// <param name="displayName">Optional display name, if it was set by the endpoint of that remote participant.</param>
        public RemoteParticipant(CommunicationUserIdentifier identifier, string? displayName)
            : base(identifier, displayName)
        {
        }
    }
}
