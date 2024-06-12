//-----------------------------------------------------------------------
// <copyright file="AudioDeviceInfo.cs" company="P.O.S Informatique">
//     Copyright (c) P.O.S Informatique. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace PosInformatique.Azure.Communication.UI.Blazor
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// Information about a microphone or speaker device.
    /// </summary>
    public class AudioDeviceInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AudioDeviceInfo"/> class.
        /// </summary>
        /// <param name="id">The id of this audio device.</param>
        /// <param name="name">The name of this audio device.</param>
        /// <param name="deviceType">The audio device type.</param>
        /// <param name="isSystemDefault">Indicating whether if the audio device is systems default.</param>
        public AudioDeviceInfo(string id, string name, AudioDeviceType deviceType, bool isSystemDefault)
        {
            this.Id = id;
            this.Name = name;
            this.DeviceType = deviceType;
            this.IsSystemDefault = isSystemDefault;
        }

        /// <summary>
        /// Gets the name of this audio device.
        /// </summary>
        [JsonPropertyName("name")]
        [JsonPropertyOrder(1)]
        public string Name { get; }

        /// <summary>
        /// Gets the id of this audio device.
        /// </summary>
        [JsonPropertyName("id")]
        [JsonPropertyOrder(2)]
        public string Id { get; }

        /// <summary>
        /// Gets a value indicating whether if the audio device is systems default.
        /// </summary>
        [JsonPropertyName("isSystemDefault")]
        [JsonPropertyOrder(3)]
        public bool IsSystemDefault { get; }

        /// <summary>
        /// Gets the audio device type.
        /// </summary>
        [JsonPropertyName("deviceType")]
        [JsonPropertyOrder(4)]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public AudioDeviceType DeviceType { get; }
    }
}
