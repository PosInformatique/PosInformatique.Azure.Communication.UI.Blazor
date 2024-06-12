//-----------------------------------------------------------------------
// <copyright file="AudioDeviceInfoTest.cs" company="P.O.S Informatique">
//     Copyright (c) P.O.S Informatique. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace PosInformatique.Azure.Communication.UI.Blazor.Tests
{
    public class AudioDeviceInfoTest
    {
        [Fact]
        public void Constructor()
        {
            var deviceInfo = new AudioDeviceInfo("The id", "The name", AudioDeviceType.Speaker, true);

            deviceInfo.DeviceType.Should().Be(AudioDeviceType.Speaker);
            deviceInfo.Id.Should().Be("The id");
            deviceInfo.IsSystemDefault.Should().BeTrue();
            deviceInfo.Name.Should().Be("The name");
        }

        [Fact]
        public void Serialization()
        {
            var deviceInfo = new AudioDeviceInfo("The id", "The name", AudioDeviceType.Speaker, true);

            deviceInfo.Should().BeJsonSerializableInto(new
            {
                name = "The name",
                id = "The id",
                isSystemDefault = true,
                deviceType = "Speaker",
            });
        }

        [Fact]
        public void Deserialization()
        {
            var json = new
            {
                name = "The name",
                id = "The id",
                isSystemDefault = true,
                deviceType = "Speaker",
            };

            json.Should().BeJsonDeserializableInto(new AudioDeviceInfo("The id", "The name", AudioDeviceType.Speaker, true));
        }
    }
}