//-----------------------------------------------------------------------
// <copyright file="JoinCallOptionsTest.cs" company="P.O.S Informatique">
//     Copyright (c) P.O.S Informatique. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace PosInformatique.Azure.Communication.UI.Blazor.Tests
{
    public class JoinCallOptionsTest
    {
        [Fact]
        public void Constructor()
        {
            var options = new JoinCallOptions();

            options.CameraOn.Should().BeFalse();
            options.MicrophoneOn.Should().BeFalse();
        }

        [Fact]
        public void CameraOn_ValueChanged()
        {
            var options = new JoinCallOptions();

            options.CameraOn = true;

            options.CameraOn.Should().BeTrue();
        }

        [Fact]
        public void MicrophoneOn_ValueChanged()
        {
            var options = new JoinCallOptions();

            options.MicrophoneOn = true;

            options.MicrophoneOn.Should().BeTrue();
        }

        [Fact]
        public void Serialization()
        {
            var options = new JoinCallOptions()
            {
                CameraOn = true,
                MicrophoneOn = true,
            };

            options.Should().BeJsonSerializableInto(new
            {
                microphoneOn = true,
                cameraOn = true,
            });
        }

        [Fact]
        public void Deserialization()
        {
            var json = new
            {
                microphoneOn = true,
                cameraOn = true,
            };

            json.Should().BeJsonDeserializableInto(new JoinCallOptions()
            {
                CameraOn = true,
                MicrophoneOn = true,
            });
        }
    }
}