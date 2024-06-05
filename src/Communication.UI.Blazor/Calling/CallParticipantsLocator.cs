//-----------------------------------------------------------------------
// <copyright file="CallParticipantsLocator.cs" company="P.O.S Informatique">
//     Copyright (c) P.O.S Informatique. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace PosInformatique.Azure.Communication.UI.Blazor
{
    using System.Collections.ObjectModel;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Locator used to call one or more participants.
    /// </summary>
    public class CallParticipantsLocator : CallAdapterLocator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CallParticipantsLocator"/> class.
        /// </summary>
        public CallParticipantsLocator()
        {
            this.ParticipantIds = new Collection<string>();
        }

        /// <summary>
        /// Gets or sets the participant identifiers to call.
        /// </summary>
        [JsonPropertyName("participantIds")]
        [JsonPropertyOrder(1)]
        public Collection<string> ParticipantIds { get; set; }
    }
}
