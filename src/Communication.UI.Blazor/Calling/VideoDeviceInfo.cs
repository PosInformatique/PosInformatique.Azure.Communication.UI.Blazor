//-----------------------------------------------------------------------
// <copyright file="VideoDeviceInfo.cs" company="P.O.S Informatique">
//     Copyright (c) P.O.S Informatique. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace PosInformatique.Azure.Communication.UI.Blazor
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// Information about a camera device.
    /// </summary>
    public class VideoDeviceInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VideoDeviceInfo"/> class.
        /// </summary>
        /// <param name="id">The id of this video device.</param>
        /// <param name="name">The name of this video device.</param>
        /// <param name="deviceType">The video device type.</param>
        public VideoDeviceInfo(string id, string name, VideoDeviceType deviceType)
        {
            this.Id = id;
            this.Name = name;
            this.DeviceType = deviceType;
        }

        /// <summary>
        /// Gets the name of this video device.
        /// </summary>
        [JsonPropertyName("name")]
        [JsonPropertyOrder(1)]
        public string Name { get; }

        /// <summary>
        /// Gets the id of this video device.
        /// </summary>
        [JsonPropertyName("id")]
        [JsonPropertyOrder(2)]
        public string Id { get; }

        /// <summary>
        /// Gets the video device type.
        /// </summary>
        [JsonPropertyName("deviceType")]
        [JsonPropertyOrder(3)]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public VideoDeviceType DeviceType { get; }
    }
}
