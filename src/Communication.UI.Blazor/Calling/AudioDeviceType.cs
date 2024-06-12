//-----------------------------------------------------------------------
// <copyright file="AudioDeviceType.cs" company="P.O.S Informatique">
//     Copyright (c) P.O.S Informatique. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace PosInformatique.Azure.Communication.UI.Blazor
{
    /// <summary>
    /// Type of an audio device.
    /// </summary>
    public enum AudioDeviceType
    {
        /// <summary>
        /// The audio device is microphone.
        /// </summary>
        Microphone = 0,

        /// <summary>
        /// The audio device is speaker.
        /// </summary>
        Speaker = 1,

        /// <summary>
        /// The audio device is a composite audio device.
        /// </summary>
        CompositeAudioDevice = 2,

        /// <summary>
        /// The audio device is virtual (simulated by the host device).
        /// </summary>
        Virtual = 3,
    }
}
