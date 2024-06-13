//-----------------------------------------------------------------------
// <copyright file="CallAdapterState.cs" company="P.O.S Informatique">
//     Copyright (c) P.O.S Informatique. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace PosInformatique.Azure.Communication.UI.Blazor
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// Represents the current <see cref="CallAdapter"/> state.
    /// </summary>
    public class CallAdapterState
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CallAdapterState"/> class.
        /// </summary>
        /// <param name="userId">The <see cref="CommunicationUserKind"/> of the current user.</param>
        public CallAdapterState(CommunicationUserKind userId)
        {
            this.UserId = userId;
        }

        /// <summary>
        /// Gets the <see cref="CommunicationUserKind"/> of the current user.
        /// </summary>
        [JsonPropertyName("userId")]
        [JsonPropertyOrder(1)]
        public CommunicationUserKind UserId { get; }

        /// <summary>
        /// Gets the display name of the current user.
        /// </summary>
        [JsonPropertyName("displayName")]
        [JsonPropertyOrder(2)]
        [JsonInclude]
        public string? DisplayName { get; init; }

        /// <summary>
        /// Gets a value indicating whether if the current call is Teams call.
        /// </summary>
        [JsonPropertyName("isTeamsCall")]
        [JsonPropertyOrder(7)]
        [JsonInclude]
        public bool IsTeamsCall { get; init; }

        /// <summary>
        /// Gets a value indicating whether if the call is a rooms call.
        /// </summary>
        [JsonPropertyName("isRoomsCall")]
        [JsonPropertyOrder(8)]
        [JsonInclude]
        public bool IsRoomsCall { get; init; }

        /// <summary>
        /// Gets a value indicating whether the local participant's camera is on.
        /// To be used when creating a custom control bar with the CallComposite.
        /// </summary>
        [JsonPropertyName("cameraStatus")]
        [JsonPropertyOrder(12)]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        [JsonInclude]
        public CameraStatus? CameraStatus { get; init; }

        /// <summary>
        /// Gets a value indicating whether if the microphone is enabled.
        /// </summary>
        [JsonPropertyName("isLocalPreviewMicrophoneEnabled")]
        [JsonPropertyOrder(50)]
        [JsonInclude]
        public bool IsLocalPreviewMicrophoneEnabled { get; init; }

        /// <summary>
        /// Gets the current page display on the <see cref="CallComposite"/>.
        /// </summary>
        [JsonPropertyName("page")]
        [JsonPropertyOrder(51)]
        [JsonConverter(typeof(JsonCamelCaseStringEnumConverter))]
        [JsonInclude]
        public CallCompositePage Page { get; init; }
    }
}
