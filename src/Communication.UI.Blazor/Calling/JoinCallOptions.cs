//-----------------------------------------------------------------------
// <copyright file="JoinCallOptions.cs" company="P.O.S Informatique">
//     Copyright (c) P.O.S Informatique. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace PosInformatique.Azure.Communication.UI.Blazor
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// Options for joining a group call.
    /// </summary>
    public class JoinCallOptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JoinCallOptions"/> class.
        /// </summary>
        public JoinCallOptions()
        {
        }

        /// <summary>
        /// Gets or sets a value indicating whether if the microphone is turned on.
        /// </summary>
        [JsonPropertyName("microphoneOn")]
        [JsonPropertyOrder(1)]
        public bool MicrophoneOn { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether if the camera is turned on.
        /// </summary>
        [JsonPropertyName("cameraOn")]
        [JsonPropertyOrder(2)]
        public bool CameraOn { get; set; }
    }
}
