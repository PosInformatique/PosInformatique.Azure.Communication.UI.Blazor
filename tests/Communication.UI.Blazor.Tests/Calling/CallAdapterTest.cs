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
        public void Constructor()
        {
            var module = Mock.Of<IJSObjectReference>();

            var callAdapter = new CallAdapter(module);

            callAdapter.Id.Should().NotBeEmpty();
            callAdapter.Module.Should().BeSameAs(module);
        }

        [Fact]
        public async Task DisposeAsync()
        {
            var elementReference = new ElementReference("The id");

            var args = new CallAdapterArgs(default, default, default);

            var module = new Mock<IJSObjectReference>(MockBehavior.Strict);
            module.Setup(m => m.InvokeAsync<Microsoft.JSInterop.Infrastructure.IJSVoidResult>("dispose", It.IsAny<object[]>()))
                .ReturnsAsync((Microsoft.JSInterop.Infrastructure.IJSVoidResult)null);

            var callAdapter = new CallAdapter(module.Object);

            await callAdapter.DisposeAsync();

            await callAdapter.Invoking(c => c.JoinCallAsync(default))
                .Should().ThrowExactlyAsync<ObjectDisposedException>()
                .WithMessage("Cannot access a disposed object.\r\nObject name: 'PosInformatique.Azure.Communication.UI.Blazor.CallAdapter'.");

            await callAdapter.Invoking(c => c.LeaveCallAsync(default))
                .Should().ThrowExactlyAsync<ObjectDisposedException>()
                .WithMessage("Cannot access a disposed object.\r\nObject name: 'PosInformatique.Azure.Communication.UI.Blazor.CallAdapter'.");

            await callAdapter.Invoking(c => c.MuteAsync())
                .Should().ThrowExactlyAsync<ObjectDisposedException>()
                .WithMessage("Cannot access a disposed object.\r\nObject name: 'PosInformatique.Azure.Communication.UI.Blazor.CallAdapter'.");

            await callAdapter.Invoking(c => c.QueryCamerasAsync())
                .Should().ThrowExactlyAsync<ObjectDisposedException>()
                .WithMessage("Cannot access a disposed object.\r\nObject name: 'PosInformatique.Azure.Communication.UI.Blazor.CallAdapter'.");

            await callAdapter.Invoking(c => c.QueryMicrophonesAsync())
                .Should().ThrowExactlyAsync<ObjectDisposedException>()
                .WithMessage("Cannot access a disposed object.\r\nObject name: 'PosInformatique.Azure.Communication.UI.Blazor.CallAdapter'.");

            await callAdapter.Invoking(c => c.QuerySpeakersAsync())
                .Should().ThrowExactlyAsync<ObjectDisposedException>()
                .WithMessage("Cannot access a disposed object.\r\nObject name: 'PosInformatique.Azure.Communication.UI.Blazor.CallAdapter'.");

            await callAdapter.Invoking(c => c.StartScreenShareAsync())
                .Should().ThrowExactlyAsync<ObjectDisposedException>()
                .WithMessage("Cannot access a disposed object.\r\nObject name: 'PosInformatique.Azure.Communication.UI.Blazor.CallAdapter'.");

            await callAdapter.Invoking(c => c.StopScreenShareAsync())
                .Should().ThrowExactlyAsync<ObjectDisposedException>()
                .WithMessage("Cannot access a disposed object.\r\nObject name: 'PosInformatique.Azure.Communication.UI.Blazor.CallAdapter'.");

            await callAdapter.Invoking(c => c.UnmuteAsync())
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
            var endedEvent = new CallEndedEvent(default);
            var onCallEndedCalled = false;

            adapter.OnCallEnded += new AsyncEventHandler<CallEndedEvent>(e =>
            {
                e.Should().BeSameAs(endedEvent);
                onCallEndedCalled = true;

                return Task.CompletedTask;
            });

            callBackReference.Invoke("OnCallEndedAsync", endedEvent);

            onCallEndedCalled.Should().BeTrue();

            // Check the On, event
            var muteEvent = new MicrophoneMuteChangedEvent(default, default);
            var onMicrophoneMuteChanged = false;

            adapter.OnMicrophoneMuteChanged += new AsyncEventHandler<MicrophoneMuteChangedEvent>(e =>
            {
                e.Should().BeSameAs(muteEvent);
                onMicrophoneMuteChanged = true;

                return Task.CompletedTask;
            });

            callBackReference.Invoke("OnMicrophoneMuteChangedAsync", muteEvent);

            onMicrophoneMuteChanged.Should().BeTrue();

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

        [Fact]
        public async Task LeaveCallAsyncAsync()
        {
            var options = new JoinCallOptions();

            var module = new Mock<IJSObjectReference>(MockBehavior.Strict);
            module.Setup(m => m.InvokeAsync<Microsoft.JSInterop.Infrastructure.IJSVoidResult>("adapterLeaveCall", It.IsAny<object[]>()))
                .Callback((string _, object[] a) =>
                {
                    a.Should().HaveCount(2);
                    a[0].As<Guid>().Should().NotBeEmpty();
                    a[1].Should().Be(true);
                })
                .ReturnsAsync((Microsoft.JSInterop.Infrastructure.IJSVoidResult)null);

            var adapter = new CallAdapter(module.Object);

            await adapter.LeaveCallAsync(true);

            module.VerifyAll();
        }

        [Fact]
        public async Task LeaveAsync_AlreadyDisposed()
        {
            var adapter = new CallAdapter(default);

            adapter.Dispose();

            await adapter.Invoking(c => c.LeaveCallAsync(default))
                .Should().ThrowExactlyAsync<ObjectDisposedException>()
                .WithMessage("Cannot access a disposed object.\r\nObject name: 'PosInformatique.Azure.Communication.UI.Blazor.CallAdapter'.");
        }

        [Fact]
        public async Task MuteAsync()
        {
            var module = new Mock<IJSObjectReference>(MockBehavior.Strict);
            module.Setup(m => m.InvokeAsync<Microsoft.JSInterop.Infrastructure.IJSVoidResult>("adapterMute", It.IsAny<object[]>()))
                .Callback((string _, object[] a) =>
                {
                    a.Should().HaveCount(1);
                    a[0].As<Guid>().Should().NotBeEmpty();
                })
                .ReturnsAsync((Microsoft.JSInterop.Infrastructure.IJSVoidResult)null);

            var adapter = new CallAdapter(module.Object);

            await adapter.MuteAsync();

            module.VerifyAll();
        }

        [Fact]
        public async Task MuteAsync_AlreadyDisposed()
        {
            var adapter = new CallAdapter(default);

            adapter.Dispose();

            await adapter.Invoking(c => c.MuteAsync())
                .Should().ThrowExactlyAsync<ObjectDisposedException>()
                .WithMessage("Cannot access a disposed object.\r\nObject name: 'PosInformatique.Azure.Communication.UI.Blazor.CallAdapter'.");
        }

        [Fact]
        public async Task QueryCamerasAsync()
        {
            var devices = Array.Empty<VideoDeviceInfo>();

            var module = new Mock<IJSObjectReference>(MockBehavior.Strict);
            module.Setup(m => m.InvokeAsync<IReadOnlyList<VideoDeviceInfo>>("adapterQueryCameras", It.IsAny<object[]>()))
                .Callback((string _, object[] a) =>
                {
                    a.Should().HaveCount(1);
                    a[0].As<Guid>().Should().NotBeEmpty();
                })
                .ReturnsAsync(devices);

            var adapter = new CallAdapter(module.Object);

            var result = await adapter.QueryCamerasAsync();

            result.Should().BeSameAs(devices);

            module.VerifyAll();
        }

        [Fact]
        public async Task QueryCamerasAsync_AlreadyDisposed()
        {
            var adapter = new CallAdapter(default);

            adapter.Dispose();

            await adapter.Invoking(c => c.QueryCamerasAsync())
                .Should().ThrowExactlyAsync<ObjectDisposedException>()
                .WithMessage("Cannot access a disposed object.\r\nObject name: 'PosInformatique.Azure.Communication.UI.Blazor.CallAdapter'.");
        }

        [Fact]
        public async Task QueryMicrophonesAsync()
        {
            var devices = Array.Empty<AudioDeviceInfo>();

            var module = new Mock<IJSObjectReference>(MockBehavior.Strict);
            module.Setup(m => m.InvokeAsync<IReadOnlyList<AudioDeviceInfo>>("adapterQueryMicrophones", It.IsAny<object[]>()))
                .Callback((string _, object[] a) =>
                {
                    a.Should().HaveCount(1);
                    a[0].As<Guid>().Should().NotBeEmpty();
                })
                .ReturnsAsync(devices);

            var adapter = new CallAdapter(module.Object);

            var result = await adapter.QueryMicrophonesAsync();

            result.Should().BeSameAs(devices);

            module.VerifyAll();
        }

        [Fact]
        public async Task QueryMicrophonesAsync_AlreadyDisposed()
        {
            var adapter = new CallAdapter(default);

            adapter.Dispose();

            await adapter.Invoking(c => c.QueryMicrophonesAsync())
                .Should().ThrowExactlyAsync<ObjectDisposedException>()
                .WithMessage("Cannot access a disposed object.\r\nObject name: 'PosInformatique.Azure.Communication.UI.Blazor.CallAdapter'.");
        }

        [Fact]
        public async Task QuerySpeakersAsync()
        {
            var devices = Array.Empty<AudioDeviceInfo>();

            var module = new Mock<IJSObjectReference>(MockBehavior.Strict);
            module.Setup(m => m.InvokeAsync<IReadOnlyList<AudioDeviceInfo>>("adapterQuerySpeakers", It.IsAny<object[]>()))
                .Callback((string _, object[] a) =>
                {
                    a.Should().HaveCount(1);
                    a[0].As<Guid>().Should().NotBeEmpty();
                })
                .ReturnsAsync(devices);

            var adapter = new CallAdapter(module.Object);

            var result = await adapter.QuerySpeakersAsync();

            result.Should().BeSameAs(devices);

            module.VerifyAll();
        }

        [Fact]
        public async Task QuerySpeakersAsync_AlreadyDisposed()
        {
            var adapter = new CallAdapter(default);

            adapter.Dispose();

            await adapter.Invoking(c => c.QuerySpeakersAsync())
                .Should().ThrowExactlyAsync<ObjectDisposedException>()
                .WithMessage("Cannot access a disposed object.\r\nObject name: 'PosInformatique.Azure.Communication.UI.Blazor.CallAdapter'.");
        }

        [Fact]
        public async Task StartScreenShareAsync()
        {
            var module = new Mock<IJSObjectReference>(MockBehavior.Strict);
            module.Setup(m => m.InvokeAsync<Microsoft.JSInterop.Infrastructure.IJSVoidResult>("adapterStartScreenShare", It.IsAny<object[]>()))
                .Callback((string _, object[] a) =>
                {
                    a.Should().HaveCount(1);
                    a[0].As<Guid>().Should().NotBeEmpty();
                })
                .ReturnsAsync((Microsoft.JSInterop.Infrastructure.IJSVoidResult)null);

            var adapter = new CallAdapter(module.Object);

            await adapter.StartScreenShareAsync();

            module.VerifyAll();
        }

        [Fact]
        public async Task StartScreenShareAsync_AlreadyDisposed()
        {
            var adapter = new CallAdapter(default);

            adapter.Dispose();

            await adapter.Invoking(c => c.StartScreenShareAsync())
                .Should().ThrowExactlyAsync<ObjectDisposedException>()
                .WithMessage("Cannot access a disposed object.\r\nObject name: 'PosInformatique.Azure.Communication.UI.Blazor.CallAdapter'.");
        }

        [Fact]
        public async Task StopScreenShareAsync()
        {
            var module = new Mock<IJSObjectReference>(MockBehavior.Strict);
            module.Setup(m => m.InvokeAsync<Microsoft.JSInterop.Infrastructure.IJSVoidResult>("adapterStopScreenShare", It.IsAny<object[]>()))
                .Callback((string _, object[] a) =>
                {
                    a.Should().HaveCount(1);
                    a[0].As<Guid>().Should().NotBeEmpty();
                })
                .ReturnsAsync((Microsoft.JSInterop.Infrastructure.IJSVoidResult)null);

            var adapter = new CallAdapter(module.Object);

            await adapter.StopScreenShareAsync();

            module.VerifyAll();
        }

        [Fact]
        public async Task StopScreenShareAsync_AlreadyDisposed()
        {
            var adapter = new CallAdapter(default);

            adapter.Dispose();

            await adapter.Invoking(c => c.StopScreenShareAsync())
                .Should().ThrowExactlyAsync<ObjectDisposedException>()
                .WithMessage("Cannot access a disposed object.\r\nObject name: 'PosInformatique.Azure.Communication.UI.Blazor.CallAdapter'.");
        }

        [Fact]
        public async Task UnmuteAsync()
        {
            var module = new Mock<IJSObjectReference>(MockBehavior.Strict);
            module.Setup(m => m.InvokeAsync<Microsoft.JSInterop.Infrastructure.IJSVoidResult>("adapterUnmute", It.IsAny<object[]>()))
                .Callback((string _, object[] a) =>
                {
                    a.Should().HaveCount(1);
                    a[0].As<Guid>().Should().NotBeEmpty();
                })
                .ReturnsAsync((Microsoft.JSInterop.Infrastructure.IJSVoidResult)null);

            var adapter = new CallAdapter(module.Object);

            await adapter.UnmuteAsync();

            module.VerifyAll();
        }

        [Fact]
        public async Task UnmuteAsync_AlreadyDisposed()
        {
            var adapter = new CallAdapter(default);

            adapter.Dispose();

            await adapter.Invoking(c => c.UnmuteAsync())
                .Should().ThrowExactlyAsync<ObjectDisposedException>()
                .WithMessage("Cannot access a disposed object.\r\nObject name: 'PosInformatique.Azure.Communication.UI.Blazor.CallAdapter'.");
        }
    }
}