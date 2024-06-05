//-----------------------------------------------------------------------
// <copyright file="CallAdapterTest.cs" company="P.O.S Informatique">
//     Copyright (c) P.O.S Informatique. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace PosInformatique.Azure.Communication.UI.Blazor.Tests
{
    using Microsoft.AspNetCore.Components;
    using Microsoft.JSInterop;

    public class CallAdapterTest
    {
        [Fact]
        public async Task DisposeAsync()
        {
            var elementReference = new ElementReference("The id");

            var args = new CallAdapterArgs(default, default, default);

            var module = new Mock<IJSObjectReference>(MockBehavior.Strict);
            module.Setup(m => m.InvokeAsync<Microsoft.JSInterop.Infrastructure.IJSVoidResult>("dispose", It.IsAny<object[]>()))
                .ReturnsAsync((Microsoft.JSInterop.Infrastructure.IJSVoidResult)null);
            module.Setup(m => m.DisposeAsync())
                .Returns(ValueTask.CompletedTask);

            var callAdapter = new CallAdapter(module.Object);

            await callAdapter.DisposeAsync();

            await callAdapter.Invoking(c => c.JoinCallAsync(default))
                .Should().ThrowExactlyAsync<ObjectDisposedException>()
                .WithMessage("Cannot access a disposed object.\r\nObject name: 'PosInformatique.Azure.Communication.UI.Blazor.CallAdapter'.");

            module.VerifyAll();
        }

        [Fact]
        public async Task InitializeAsync()
        {
            var args = new CallAdapterArgs(default, default, default);

            object callBackReference = null;

            var module = new Mock<IJSObjectReference>(MockBehavior.Strict);
            module.Setup(m => m.InvokeAsync<Microsoft.JSInterop.Infrastructure.IJSVoidResult>("createCallAdapter", It.IsAny<object[]>()))
                .Callback((string _, object[] a) =>
                {
                    a.Should().HaveCount(3);
                    a[0].As<Guid>().Should().NotBeEmpty();
                    a[1].Should().BeSameAs(args);

                    callBackReference = a[2].GetPropertyValue("Value");
                })
                .ReturnsAsync((Microsoft.JSInterop.Infrastructure.IJSVoidResult)null);

            var adapter = new CallAdapter(module.Object);

            await adapter.InitializeAsync(args);

            // Check the OnCallEnded event
            var endedEvent = new CallAdapterCallEndedEvent(default);

            adapter.OnCallEnded += new AsyncEventHandler<CallAdapterCallEndedEvent>(e =>
            {
                e.Should().BeSameAs(endedEvent);

                return Task.CompletedTask;
            });

            callBackReference.Invoke("OnCallEndedAsync", endedEvent);

            // Check the OnParticipantsJoinedAsync event
            var count = 0;

            var joinedParticipant = new[]
            {
                new RemoteParticipant(default, default),
                new RemoteParticipant(default, default),
            };

            adapter.OnParticipantJoined += new AsyncEventHandler<RemoteParticipantJoinedEvent>(e =>
            {
                e.Participant.Should().BeSameAs(joinedParticipant[count++]);

                return Task.CompletedTask;
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

            adapter.OnParticipantLeft += new AsyncEventHandler<RemoteParticipantLeftEvent>(e =>
            {
                e.Participant.Should().BeSameAs(removedParticipant[count++]);

                return Task.CompletedTask;
            });

            callBackReference.Invoke("OnParticipantsLeftAsync", [removedParticipant]);

            count.Should().Be(2);

            module.VerifyAll();
        }

        [Fact]
        public async Task InitializeControlAsync()
        {
            var callContainer = new ElementReference("The element");

            var module = new Mock<IJSObjectReference>(MockBehavior.Strict);
            module.Setup(m => m.InvokeAsync<Microsoft.JSInterop.Infrastructure.IJSVoidResult>("initializeControl", It.IsAny<object[]>()))
                .Callback((string _, object[] a) =>
                {
                    a.Should().HaveCount(2);
                    a[0].As<Guid>().Should().NotBeEmpty();
                    a[1].Should().Be(callContainer);
                })
                .ReturnsAsync((Microsoft.JSInterop.Infrastructure.IJSVoidResult)null);

            var adapter = new CallAdapter(module.Object);

            await adapter.InitializeControlAsync(callContainer);

            module.VerifyAll();
        }

        [Fact]
        public async Task JoinCallAsync()
        {
            var options = new JoinCallOptions();

            var module = new Mock<IJSObjectReference>(MockBehavior.Strict);
            module.Setup(m => m.InvokeAsync<Microsoft.JSInterop.Infrastructure.IJSVoidResult>("adapterJoinCall", It.IsAny<object[]>()))
                .Callback((string _, object[] a) =>
                {
                    a.Should().HaveCount(2);
                    a[0].As<Guid>().Should().NotBeEmpty();
                    a[1].Should().BeSameAs(options);
                })
                .ReturnsAsync((Microsoft.JSInterop.Infrastructure.IJSVoidResult)null);

            var adapter = new CallAdapter(module.Object);

            await adapter.JoinCallAsync(options);

            module.VerifyAll();
        }

        [Fact]
        public async Task JoinAsync_AlreadyDisposed()
        {
            var adapter = new CallAdapter(default);

            adapter.Dispose();

            await adapter.Invoking(c => c.JoinCallAsync(default))
                .Should().ThrowExactlyAsync<ObjectDisposedException>()
                .WithMessage("Cannot access a disposed object.\r\nObject name: 'PosInformatique.Azure.Communication.UI.Blazor.CallAdapter'.");
        }
    }
}