//-----------------------------------------------------------------------
// <copyright file="CallCompositeTest.cs" company="P.O.S Informatique">
//     Copyright (c) P.O.S Informatique. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace PosInformatique.Azure.Communication.UI.Blazor.Tests
{
    using AngleSharp.Dom;
    using Bunit;
    using Microsoft.AspNetCore.Components;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.JSInterop;

    public class CallCompositeTest : TestContext
    {
        [Fact]
        public void Constructor()
        {
            var callComposite = new CallComposite();

            callComposite.Adapter.Should().BeNull();
            callComposite.CameraButton.Should().BeTrue();
            callComposite.DevicesButton.Should().BeTrue();
            callComposite.EndCallButton.Should().BeTrue();
            callComposite.MicrophoneButton.Should().BeTrue();
            callComposite.MoreButton.Should().BeTrue();
            callComposite.ParticipantsButton.Should().BeTrue();
            callComposite.PeopleButton.Should().BeTrue();
            callComposite.RaiseHandButton.Should().BeTrue();
            callComposite.ScreenShareButton.Should().BeTrue();
        }

        [Fact]
        public void Adapter_ValueChanged()
        {
            var callComposite = new CallComposite();

            var adapter = Mock.Of<ICallAdapter>();

            callComposite.Adapter = adapter;

            callComposite.Adapter.Should().BeSameAs(adapter);
        }

        [Fact]
        public void CameraButton_ValueChanged()
        {
            var callComposite = new CallComposite();

            callComposite.CameraButton = true;

            callComposite.CameraButton.Should().BeTrue();
        }

        [Fact]
        public void DevicesButton_ValueChanged()
        {
            var callComposite = new CallComposite();

            callComposite.DevicesButton = true;

            callComposite.DevicesButton.Should().BeTrue();
        }

        [Fact]
        public void EndCallButton_ValueChanged()
        {
            var callComposite = new CallComposite();

            callComposite.EndCallButton = true;

            callComposite.EndCallButton.Should().BeTrue();
        }

        [Fact]
        public void MicrophoneButton_ValueChanged()
        {
            var callComposite = new CallComposite();

            callComposite.MicrophoneButton = true;

            callComposite.MicrophoneButton.Should().BeTrue();
        }

        [Fact]
        public void MoreButton_ValueChanged()
        {
            var callComposite = new CallComposite();

            callComposite.MoreButton = true;

            callComposite.MoreButton.Should().BeTrue();
        }

        [Fact]
        public void ParticipantsButton_ValueChanged()
        {
            var callComposite = new CallComposite();

            callComposite.ParticipantsButton = true;

            callComposite.ParticipantsButton.Should().BeTrue();
        }

        [Fact]
        public void PeopleButton_ValueChanged()
        {
            var callComposite = new CallComposite();

            callComposite.PeopleButton = true;

            callComposite.PeopleButton.Should().BeTrue();
        }

        [Fact]
        public void RaiseHandButton_ValueChanged()
        {
            var callComposite = new CallComposite();

            callComposite.RaiseHandButton = true;

            callComposite.RaiseHandButton.Should().BeTrue();
        }

        [Fact]
        public void ScreenShareButton_ValueChanged()
        {
            var callComposite = new CallComposite();

            callComposite.ScreenShareButton = true;

            callComposite.ScreenShareButton.Should().BeTrue();
        }

        [Fact]
        public void Render_WithNullAdapter()
        {
            var render = this.RenderComponent<CallComposite>(parameters =>
            {
                parameters.Add(p => p.Adapter, null);
            });

            render.MarkupMatches($"<div id=\"call-container\" style=\"height: 50vh\" blazor:elementReference=\"99b58d8c-1f85-4efd-bde6-044310bb65f3\"></div>");
        }

        [Fact]
        public void Render_WithAdapter()
        {
            ElementReference elementReference = default;
            Guid adapterId = default;

            var module = new Mock<IJSObjectReference>(MockBehavior.Strict);
            module.Setup(m => m.InvokeAsync<Microsoft.JSInterop.Infrastructure.IJSVoidResult>("initializeControl", It.IsAny<object[]>()))
                .Callback((string _, object[] a) =>
                {
                    a.Should().HaveCount(3);

                    elementReference = a[0].As<ElementReference>();
                    adapterId = a[1].As<Guid>();

                    a[2].Should().BeEquivalentTo(new CallControlOptions()
                    {
                        CameraButton = true,
                        DevicesButton = true,
                        EndCallButton = true,
                        MicrophoneButton = true,
                        MoreButton = true,
                        ParticipantsButton = true,
                        PeopleButton = true,
                        RaiseHandButton = true,
                        ScreenShareButton = true,
                    });
                })
                .ReturnsAsync((Microsoft.JSInterop.Infrastructure.IJSVoidResult)null);

            var adapter = new CallAdapter(module.Object);

            var render = this.RenderComponent<CallComposite>(parameters =>
            {
                parameters.Add(p => p.Adapter, adapter);
                parameters.Add(p => p.CameraButton, true);
                parameters.Add(p => p.DevicesButton, true);
                parameters.Add(p => p.EndCallButton, true);
                parameters.Add(p => p.MicrophoneButton, true);
                parameters.Add(p => p.MoreButton, true);
                parameters.Add(p => p.ParticipantsButton, true);
                parameters.Add(p => p.PeopleButton, true);
                parameters.Add(p => p.RaiseHandButton, true);
                parameters.Add(p => p.ScreenShareButton, true);
            });

            adapterId.Should().Be(adapter.Id);
            render.Markup.Should().Be($"<div id=\"call-container\" style=\"height: 50vh\" blazor:elementReference=\"{elementReference.Id}\"></div>");

            module.VerifyAll();
        }

        [Fact]
        public void Render_WithNoCallAdapter()
        {
            var adapter = Mock.Of<ICallAdapter>();

            this.Invoking(t => t.RenderComponent<CallComposite>(parameters =>
            {
                parameters.Add(p => p.Adapter, adapter);
            }))
                .Should().ThrowExactly<InvalidOperationException>()
                .WithMessage("The Adapter property must an instance of the CallAdapter class.");
        }
    }
}