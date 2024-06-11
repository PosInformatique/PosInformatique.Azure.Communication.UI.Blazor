//-----------------------------------------------------------------------
// <copyright file="VideoDeviceInfoTest.cs" company="P.O.S Informatique">
//     Copyright (c) P.O.S Informatique. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace PosInformatique.Azure.Communication.UI.Blazor.Tests
{
    public class VideoDeviceInfoTest
    {
        [Fact]
        public void Constructor()
        {
            var deviceInfo = new VideoDeviceInfo("The id", "The name", VideoDeviceType.CaptureAdapter);

            deviceInfo.DeviceType.Should().Be(VideoDeviceType.CaptureAdapter);
            deviceInfo.Id.Should().Be("The id");
            deviceInfo.Name.Should().Be("The name");
        }

        [Fact]
        public void Serialization()
        {
            var deviceInfo = new VideoDeviceInfo("The id", "The name", VideoDeviceType.CaptureAdapter);

            deviceInfo.Should().BeJsonSerializableInto(new
            {
                name = "The name",
                id = "The id",
                deviceType = "CaptureAdapter",
            });
        }

        [Fact]
        public void Deserialization()
        {
            var json = new
            {
                name = "The name",
                id = "The id",
                deviceType = "CaptureAdapter",
            };

            json.Should().BeJsonDeserializableInto(new VideoDeviceInfo("The id", "The name", VideoDeviceType.CaptureAdapter));
        }
    }
}