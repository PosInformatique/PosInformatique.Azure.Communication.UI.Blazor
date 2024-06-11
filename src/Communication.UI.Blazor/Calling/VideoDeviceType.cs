//-----------------------------------------------------------------------
// <copyright file="VideoDeviceType.cs" company="P.O.S Informatique">
//     Copyright (c) P.O.S Informatique. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace PosInformatique.Azure.Communication.UI.Blazor
{
    /// <summary>
    /// Type of a video device.
    /// </summary>
    public enum VideoDeviceType
    {
        /// <summary>
        /// The type of the device can not be determined.
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// The camera is simple USB camera.
        /// </summary>
        UsbCamera = 1,

        /// <summary>
        /// The camera is a capture adapter.
        /// </summary>
        CaptureAdapter = 2,

        /// <summary>
        /// The camera is virtual (simulated by the host device).
        /// </summary>
        Virtual = 3,

        /// <summary>
        /// The camera is a screen sharing.
        /// </summary>
        ScreenSharing = 4,
    }
}
