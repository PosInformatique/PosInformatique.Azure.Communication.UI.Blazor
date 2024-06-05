//-----------------------------------------------------------------------
// <copyright file="CommonCallControlOptions.cs" company="P.O.S Informatique">
//     Copyright (c) P.O.S Informatique. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace PosInformatique.Azure.Communication.UI.Blazor
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// Customization options for the control bar in calling experience.
    /// </summary>
    public abstract class CommonCallControlOptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommonCallControlOptions"/> class.
        /// </summary>
        protected CommonCallControlOptions()
        {
            this.CameraButton = true;
            this.EndCallButton = true;
            this.MicrophoneButton = true;
            this.DevicesButton = true;
            this.ParticipantsButton = true;
            this.ScreenShareButton = true;
            this.MoreButton = true;
            this.RaiseHandButton = true;
            this.PeopleButton = true;
        }

        /// <summary>
        /// Gets or sets a value indicating whether to
        /// show or hide Camera Button during a call.
        /// Default value: <see cref="true"/>.
        /// </summary>
        [JsonPropertyName("cameraButton")]
        [JsonPropertyOrder(2)]
        public bool CameraButton { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to
        /// show or hide EndCall button during a call.
        /// Default value: <see cref="true"/>.
        /// </summary>
        [JsonPropertyName("endCallButton")]
        [JsonPropertyOrder(3)]
        public bool EndCallButton { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to
        /// show or hide Microphone button during a call.
        /// Default value: <see cref="true"/>.
        /// </summary>
        [JsonPropertyName("microphoneButton")]
        [JsonPropertyOrder(4)]
        public bool MicrophoneButton { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to
        /// show or hide Devices button during a call.
        /// Default value: <see cref="true"/>.
        /// </summary>
        [JsonPropertyName("devicesButton")]
        [JsonPropertyOrder(5)]
        public bool DevicesButton { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to
        /// show, hide or disable participants button during a call.
        /// Default value: <see cref="true"/>.
        /// </summary>
        [JsonPropertyName("participantsButton")]
        [JsonPropertyOrder(6)]
        public bool ParticipantsButton { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to
        /// show, hide or disable the screen share button during a call.
        /// Default value: <see cref="true"/>.
        /// </summary>
        [JsonPropertyName("screenShareButton")]
        [JsonPropertyOrder(7)]
        public bool ScreenShareButton { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to
        /// show, hide or disable the more button during a call.
        /// Default value: <see cref="true"/>.
        /// </summary>
        [JsonPropertyName("moreButton")]
        [JsonPropertyOrder(8)]
        public bool MoreButton { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to
        /// show, hide or disable the raise hand button during a call.
        /// Default value: <see cref="true"/>.
        /// </summary>
        [JsonPropertyName("raiseHandButton")]
        [JsonPropertyOrder(9)]
        public bool RaiseHandButton { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to
        /// show, hide or disable the people button during a call.
        /// Default value: <see cref="true"/>.
        /// </summary>
        [JsonPropertyName("peopleButton")]
        [JsonPropertyOrder(11)]
        public bool PeopleButton { get; set; }
    }
}
