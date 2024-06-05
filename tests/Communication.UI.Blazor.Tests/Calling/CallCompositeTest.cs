//-----------------------------------------------------------------------
// <copyright file="CallCompositeTest.cs" company="P.O.S Informatique">
//     Copyright (c) P.O.S Informatique. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace PosInformatique.Azure.Communication.UI.Blazor.Tests
{
    using Microsoft.AspNetCore.Components;
    using Microsoft.JSInterop;

    public class CallCompositeTest
    {
        [Fact]
        public void Constructor()
        {
            var callComposite = new CallComposite();

            callComposite.IsLoaded.Should().BeFalse();
            callComposite.JSRuntime.Should().BeNull();
            callComposite.OnCallEnded.HasDelegate.Should().BeFalse();
            callComposite.OnParticipantJoined.HasDelegate.Should().BeFalse();
            callComposite.OnParticipantLeft.HasDelegate.Should().BeFalse();
        }

        [Fact]
        public async Task JoinCallAsync()
        {
            var elementReference = new ElementReference("The id");

            var options = new JoinCallOptions();

            var module = new Mock<IJSObjectReference>(MockBehavior.Strict);
            module.Setup(m => m.InvokeAsync<Microsoft.JSInterop.Infrastructure.IJSVoidResult>("initialize", It.IsAny<object[]>()))
                .ReturnsAsync((Microsoft.JSInterop.Infrastructure.IJSVoidResult)null);
            module.Setup(m => m.InvokeAsync<Microsoft.JSInterop.Infrastructure.IJSVoidResult>("adapterJoinCall", It.IsAny<object[]>()))
                .Callback((string _, object[] a) =>
                {
                    a.Should().HaveCount(2);
                    a[0].Should().Be(elementReference);
                    a[1].Should().BeSameAs(options);
                })
                .ReturnsAsync((Microsoft.JSInterop.Infrastructure.IJSVoidResult)null);

            var jsRuntime = new Mock<IJSRuntime>(MockBehavior.Strict);
            jsRuntime.Setup(j => j.InvokeAsync<IJSObjectReference>("import", It.Is<object[]>(args => (string)args[0] == "./_content/PosInformatique.Azure.Communication.UI.Blazor/Calling/CallComposite.razor.js")))
                .ReturnsAsync(module.Object);

            var callComposite = new CallComposite()
            {
                JSRuntime = jsRuntime.Object,
            };

            callComposite.SetFieldValue("callContainer", elementReference);

            await callComposite.LoadAsync(default);

            await callComposite.JoinCallAsync(options);

            callComposite.IsLoaded.Should().BeTrue();

            jsRuntime.VerifyAll();
            module.VerifyAll();
        }

        [Fact]
        public async Task JoinCallAsync_NotLoaded()
        {
            var options = new JoinCallOptions();

            var callComposite = new CallComposite();

            await callComposite.Invoking(c => c.JoinCallAsync(options))
                .Should().ThrowExactlyAsync<InvalidOperationException>()
                .WithMessage("The component has not been loaded. Ensures that the LoadAsync() method has been called first.");
        }

        [Fact]
        public async Task JoinAsync_AlreadyDisposed()
        {
            var callComposite = new CallComposite();

            callComposite.Dispose();

            await callComposite.Invoking(c => c.JoinCallAsync(default))
                .Should().ThrowExactlyAsync<ObjectDisposedException>()
                .WithMessage("Cannot access a disposed object.\r\nObject name: 'PosInformatique.Azure.Communication.UI.Blazor.CallComposite'.");
        }

        [Fact]
        public async Task LoadAsync()
        {
            var elementReference = new ElementReference("The id");

            var args = new CallAdapterArgs(default, default, default);

            object callBackReference = null;

            var module = new Mock<IJSObjectReference>(MockBehavior.Strict);
            module.Setup(m => m.InvokeAsync<Microsoft.JSInterop.Infrastructure.IJSVoidResult>("initialize", It.IsAny<object[]>()))
                .Callback((string _, object[] a) =>
                {
                    a.Should().HaveCount(3);
                    a[0].Should().Be(elementReference);
                    a[1].Should().BeSameAs(args);

                    callBackReference = a[2].GetPropertyValue("Value");
                })
                .ReturnsAsync((Microsoft.JSInterop.Infrastructure.IJSVoidResult)null);

            var jsRuntime = new Mock<IJSRuntime>(MockBehavior.Strict);
            jsRuntime.Setup(j => j.InvokeAsync<IJSObjectReference>("import", It.Is<object[]>(args => (string)args[0] == "./_content/PosInformatique.Azure.Communication.UI.Blazor/Calling/CallComposite.razor.js")))
                .ReturnsAsync(module.Object);

            var callComposite = new CallComposite()
            {
                JSRuntime = jsRuntime.Object,
            };

            callComposite.SetFieldValue("callContainer", elementReference);

            await callComposite.LoadAsync(args);

            callComposite.IsLoaded.Should().BeTrue();

            // Check the OnCallEnded event
            var endedEvent = new CallAdapterCallEndedEvent(default);

            callComposite.OnCallEnded = new EventCallback<CallAdapterCallEndedEvent>(null, (CallAdapterCallEndedEvent e) =>
            {
                e.Should().BeSameAs(endedEvent);
            });

            callBackReference.Invoke("OnCallEndedAsync", endedEvent);

            // Check the OnParticipantsJoinedAsync event
            var count = 0;

            var joinedParticipant = new[]
            {
                new RemoteParticipant(default, default),
                new RemoteParticipant(default, default),
            };

            callComposite.OnParticipantJoined = new EventCallback<RemoteParticipantJoinedEvent>(null, (RemoteParticipantJoinedEvent e) =>
            {
                e.Participant.Should().BeSameAs(joinedParticipant[count++]);
            });

            callBackReference.Invoke("OnParticipantsJoinedAsync", [joinedParticipant]);

            count.Should().Be(2);

            // Check the OnParticipantsLeftAsync event
            count = 0;

            var removedParticipant = new[]
            {
                new RemoteParticipant(default, default),
                new RemoteParticipant(default, default),
            };

            callComposite.OnParticipantLeft = new EventCallback<RemoteParticipantLeftEvent>(null, (RemoteParticipantLeftEvent e) =>
            {
                e.Participant.Should().BeSameAs(removedParticipant[count++]);
            });

            callBackReference.Invoke("OnParticipantsLeftAsync", [removedParticipant]);

            count.Should().Be(2);

            jsRuntime.VerifyAll();
            module.VerifyAll();
        }

        [Fact]
        public async Task LoadAsync_AlreadyDisposed()
        {
            var callComposite = new CallComposite();

            callComposite.Dispose();

            await callComposite.Invoking(c => c.LoadAsync(default))
                .Should().ThrowExactlyAsync<ObjectDisposedException>()
                .WithMessage("Cannot access a disposed object.\r\nObject name: 'PosInformatique.Azure.Communication.UI.Blazor.CallComposite'.");
        }

        [Fact]
        public async Task DisposeAsync()
        {
            var elementReference = new ElementReference("The id");

            var args = new CallAdapterArgs(default, default, default);

            var module = new Mock<IJSObjectReference>(MockBehavior.Strict);
            module.Setup(m => m.InvokeAsync<Microsoft.JSInterop.Infrastructure.IJSVoidResult>("initialize", It.IsAny<object[]>()))
                .ReturnsAsync((Microsoft.JSInterop.Infrastructure.IJSVoidResult)null);
            module.Setup(m => m.InvokeAsync<Microsoft.JSInterop.Infrastructure.IJSVoidResult>("dispose", It.IsAny<object[]>()))
                .ReturnsAsync((Microsoft.JSInterop.Infrastructure.IJSVoidResult)null);
            module.Setup(m => m.DisposeAsync())
                .Returns(ValueTask.CompletedTask);

            var jsRuntime = new Mock<IJSRuntime>(MockBehavior.Strict);
            jsRuntime.Setup(j => j.InvokeAsync<IJSObjectReference>("import", It.Is<object[]>(args => (string)args[0] == "./_content/PosInformatique.Azure.Communication.UI.Blazor/Calling/CallComposite.razor.js")))
                .ReturnsAsync(module.Object);

            var callComposite = new CallComposite()
            {
                JSRuntime = jsRuntime.Object,
            };

            callComposite.SetFieldValue("callContainer", elementReference);

            await callComposite.LoadAsync(args);

            await callComposite.DisposeAsync();

            await callComposite.Invoking(c => c.JoinCallAsync(default))
                .Should().ThrowExactlyAsync<ObjectDisposedException>()
                .WithMessage("Cannot access a disposed object.\r\nObject name: 'PosInformatique.Azure.Communication.UI.Blazor.CallComposite'.");

            await callComposite.Invoking(c => c.LoadAsync(default))
                .Should().ThrowExactlyAsync<ObjectDisposedException>()
                .WithMessage("Cannot access a disposed object.\r\nObject name: 'PosInformatique.Azure.Communication.UI.Blazor.CallComposite'.");

            jsRuntime.VerifyAll();
            module.VerifyAll();
        }
    }
}