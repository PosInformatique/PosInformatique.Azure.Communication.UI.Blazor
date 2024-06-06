//-----------------------------------------------------------------------
// <copyright file="CallEndedEvent.cs" company="P.O.S Informatique">
//     Copyright (c) P.O.S Informatique. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace PosInformatique.Azure.Communication.UI.Blazor
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// Contains information when a call is ended.
    /// </summary>
    public class CallEndedEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CallEndedEvent"/> class.
        /// </summary>
        /// <param name="callId">Identifier of the call ended.</param>
        public CallEndedEvent(string callId)
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
