//-----------------------------------------------------------------------
// <copyright file="CallAdapterCallEndedEvent.cs" company="P.O.S Informatique">
//     Copyright (c) P.O.S Informatique. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace PosInformatique.Azure.Communication.UI.Blazor
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// Contains information when a call is ended.
    /// </summary>
    public class CallAdapterCallEndedEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CallAdapterCallEndedEvent"/> class.
        /// </summary>
        /// <param name="callId">Identifier of the call ended.</param>
        public CallAdapterCallEndedEvent(string callId)
        {
            this.CallId = callId;
        }

        /// <summary>
        /// Gets the identifier of the call ended.
        /// </summary>
        [JsonPropertyName("callId")]
        [JsonPropertyOrder(1)]
        public string CallId { get; }
    }
}
