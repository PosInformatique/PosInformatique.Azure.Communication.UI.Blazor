//-----------------------------------------------------------------------
// <copyright file="ICallAdapter.cs" company="P.O.S Informatique">
//     Copyright (c) P.O.S Informatique. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace PosInformatique.Azure.Communication.UI.Blazor
{
    using System.Threading.Tasks;

    /// <summary>
    /// Adapter which allows to manage the <see cref="CallComposite"/> component.
    /// </summary>
    public interface ICallAdapter : IAsyncDisposable
    {
        /// <summary>
        /// Occurs when the call is ended.
        /// </summary>
        event AsyncEventHandler<CallEndedEvent>? OnCallEnded;

        /// <summary>
        /// Occurs when the microphone is muted/unmuted on a participant.
        /// </summary>
        event AsyncEventHandler<MicrophoneMuteChangedEvent>? OnMicrophoneMuteChanged;

        /// <summary>
        /// Occurs when a participant join the call.
        /// </summary>
        event AsyncEventHandler<RemoteParticipantJoinedEvent>? OnParticipantJoined;

        /// <summary>
        /// Occurs when a participant leave the call.
        /// </summary>
        event AsyncEventHandler<RemoteParticipantLeftEvent>? OnParticipantLeft;

        /// <summary>
        /// Occurs when the state of the <see cref="CallAdapter"/> has been changed.
        /// </summary>
        event AsyncEventHandler<StateChangedEvent>? OnStateChanged;

        /// <summary>
        /// Get the current state of the <see cref="CallAdapter"/>.
        /// </summary>
        /// <returns>A <see cref="Task"/> that represents the asynchronous invocation which contains the <see cref="CallAdapterState"/>.</returns>
        /// <exception cref="ObjectDisposedException">If the <see cref="ICallAdapter"/> has already been disposed.</exception>
        Task<CallAdapterState> GetStateAsync();

        /// <summary>
        /// Join an existing call.
        /// </summary>
        /// <param name="options">Options of the call.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous invocation.</returns>
        /// <exception cref="ObjectDisposedException">If the <see cref="ICallAdapter"/> has already been disposed.</exception>
        Task JoinCallAsync(JoinCallOptions options);

        /// <summary>
        /// Leave the call.
        /// </summary>
        /// <param name="forEveryone">Whether to remove all participants when leaving.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous invocation.</returns>
        /// <exception cref="ObjectDisposedException">If the <see cref="ICallAdapter"/> has already been disposed.</exception>
        Task LeaveCallAsync(bool forEveryone);

        /// <summary>
        /// Remove raised hand state.
        /// </summary>
        /// <returns>A <see cref="Task"/> that represents the asynchronous invocation.</returns>
        /// <exception cref="ObjectDisposedException">If the <see cref="ICallAdapter"/> has already been disposed.</exception>
        Task LowerHandAsync();

        /// <summary>
        /// Mute the current user during the call or disable microphone locally.
        /// </summary>
        /// <returns>A <see cref="Task"/> that represents the asynchronous invocation.</returns>
        /// <exception cref="ObjectDisposedException">If the <see cref="ICallAdapter"/> has already been disposed.</exception>
        Task MuteAsync();

        /// <summary>
        /// Query for available camera devices.
        /// </summary>
        /// <returns>A <see cref="Task"/> that represents the asynchronous invocation which contains the camera devices available.</returns>
        /// <exception cref="ObjectDisposedException">If the <see cref="ICallAdapter"/> has already been disposed.</exception>
        Task<IReadOnlyList<VideoDeviceInfo>> QueryCamerasAsync();

        /// <summary>
        /// Query for available microphone devices.
        /// </summary>
        /// <returns>A <see cref="Task"/> that represents the asynchronous invocation which contains the microphone devices available.</returns>
        /// <exception cref="ObjectDisposedException">If the <see cref="ICallAdapter"/> has already been disposed.</exception>
        Task<IReadOnlyList<AudioDeviceInfo>> QueryMicrophonesAsync();

        /// <summary>
        /// Query for available speakers devices.
        /// </summary>
        /// <returns>A <see cref="Task"/> that represents the asynchronous invocation which contains the speakers devices available.</returns>
        /// <exception cref="ObjectDisposedException">If the <see cref="ICallAdapter"/> has already been disposed.</exception>
        Task<IReadOnlyList<AudioDeviceInfo>> QuerySpeakersAsync();

        /// <summary>
        /// Publish raised hand state.
        /// </summary>
        /// <returns>A <see cref="Task"/> that represents the asynchronous invocation.</returns>
        /// <exception cref="ObjectDisposedException">If the <see cref="ICallAdapter"/> has already been disposed.</exception>
        Task RaiseHandAsync();

        /// <summary>
        /// Start sharing the screen during a call.
        /// </summary>
        /// <returns>A <see cref="Task"/> that represents the asynchronous invocation.</returns>
        /// <exception cref="ObjectDisposedException">If the <see cref="ICallAdapter"/> has already been disposed.</exception>
        Task StartScreenShareAsync();

        /// <summary>
        /// Stop sharing the screen.
        /// </summary>
        /// <returns>A <see cref="Task"/> that represents the asynchronous invocation.</returns>
        /// <exception cref="ObjectDisposedException">If the <see cref="ICallAdapter"/> has already been disposed.</exception>
        Task StopScreenShareAsync();

        /// <summary>
        /// Unmute the current user during the call or enable microphone locally.
        /// </summary>
        /// <returns>A <see cref="Task"/> that represents the asynchronous invocation.</returns>
        /// <exception cref="ObjectDisposedException">If the <see cref="ICallAdapter"/> has already been disposed.</exception>
        Task UnmuteAsync();
    }
}