//-----------------------------------------------------------------------
// <copyright file="ParticipantInfo.cs" company="P.O.S Informatique">
//     Copyright (c) P.O.S Informatique. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace PosInformatique.Azure.Communication.UI.Blazor
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// Represents a participant in a call.
    /// </summary>
    [JsonDerivedType(typeof(RemoteParticipant))]
    public abstract class ParticipantInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ParticipantInfo"/> class.
        /// </summary>
        /// <param name="identifier">The identifier for this remote participant.</param>
        /// <param name="displayName">Optional display name, if it was set by the endpoint of that remote participant.</param>
        protected ParticipantInfo(CommunicationUserIdentifier identifier, string? displayName)
        {
            this.Identifier = identifier;
            this.DisplayName = displayName;
        }

        /// <summary>
        /// Gets the identifier for this remote participant.
        /// </summary>
        [JsonPropertyName("identifier")]
        [JsonPropertyOrder(1)]
        public CommunicationUserIdentifier Identifier { get; }

        /// <summary>
        /// Gets the display name, if it was set by the endpoint of that remote participant.
        /// </summary>
        [JsonPropertyName("displayName")]
        [JsonPropertyOrder(2)]
        public string? DisplayName { get; }
    }
}
