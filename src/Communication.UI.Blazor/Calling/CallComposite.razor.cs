//-----------------------------------------------------------------------
// <copyright file="CallComposite.razor.cs" company="P.O.S Informatique">
//     Copyright (c) P.O.S Informatique. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace PosInformatique.Azure.Communication.UI.Blazor
{
    using Microsoft.AspNetCore.Components;
    using Microsoft.JSInterop;

    /// <summary>
    /// Blazor component used wrap the CallComposite of Microsoft Azure Communication Services UI library.
    /// </summary>
    public sealed partial class CallComposite
    {
        private static readonly CallControlOptions DefaultOptions = new CallControlOptions();

        private ElementReference callContainer;

        private CallControlOptions? lastControlOptions;

        /// <summary>
        /// Gets or sets the <see cref="CallAdapter"/> which provides the logic and data of the composite control.
        /// The <see cref="CallComposite"/> can also be controlled using the adapter.
        /// </summary>
        [Parameter]
        [EditorRequired]
        public ICallAdapter? Adapter { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to
        /// show or hide Camera Button during a call.
        /// Default value: <see cref="true"/>.
        /// </summary>
        [Parameter]
        public bool CameraButton { get; set; } = DefaultOptions.CameraButton;

        /// <summary>
        /// Gets or sets a value indicating whether to
        /// show or hide Devices button during a call.
        /// Default value: <see cref="true"/>.
        /// </summary>
        [Parameter]
        public bool DevicesButton { get; set; } = DefaultOptions.DevicesButton;

        /// <summary>
        /// Gets or sets a value indicating whether to
        /// show or hide EndCall button during a call.
        /// Default value: <see cref="true"/>.
        /// </summary>
        [Parameter]
        public bool EndCallButton { get; set; } = DefaultOptions.EndCallButton;

        /// <summary>
        /// Gets or sets a value indicating whether to
        /// show or hide Microphone button during a call.
        /// Default value: <see cref="true"/>.
        /// </summary>
        [Parameter]
        public bool MicrophoneButton { get; set; } = DefaultOptions.MicrophoneButton;

        /// <summary>
        /// Gets or sets a value indicating whether to
        /// show, hide or disable the more button during a call.
        /// Default value: <see cref="true"/>.
        /// </summary>
        [Parameter]
        public bool MoreButton { get; set; } = DefaultOptions.MoreButton;

        /// <summary>
        /// Gets or sets a value indicating whether to
        /// show, hide or disable participants button during a call.
        /// Default value: <see cref="true"/>.
        /// </summary>
        [Parameter]
        public bool ParticipantsButton { get; set; } = DefaultOptions.ParticipantsButton;

        /// <summary>
        /// Gets or sets a value indicating whether to
        /// show, hide or disable the people button during a call.
        /// Default value: <see cref="true"/>.
        /// </summary>
        [Parameter]
        public bool PeopleButton { get; set; } = DefaultOptions.PeopleButton;

        /// <summary>
        /// Gets or sets a value indicating whether to
        /// show, hide or disable the raise hand button during a call.
        /// Default value: <see cref="true"/>.
        /// </summary>
        [Parameter]
        public bool RaiseHandButton { get; set; } = DefaultOptions.RaiseHandButton;

        /// <summary>
        /// Gets or sets a value indicating whether to
        /// show, hide or disable the screen share button during a call.
        /// Default value: <see cref="true"/>.
        /// </summary>
        [Parameter]
        public bool ScreenShareButton { get; set; } = DefaultOptions.ScreenShareButton;

        /// <inheritdoc />
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (this.Adapter is not null)
            {
                if (this.Adapter is not CallAdapter adapter)
                {
                    throw new InvalidOperationException("The Adapter property must an instance of the CallAdapter class.");
                }

                var options = new CallControlOptions()
                {
                    CameraButton = this.CameraButton,
                    DevicesButton = this.DevicesButton,
                    EndCallButton = this.EndCallButton,
                    MicrophoneButton = this.MicrophoneButton,
                    MoreButton = this.MoreButton,
                    ParticipantsButton = this.ParticipantsButton,
                    PeopleButton = this.PeopleButton,
                    RaiseHandButton = this.RaiseHandButton,
                    ScreenShareButton = this.ScreenShareButton,
                };

                if (this.ControlOptionsHasBeenChanged(options))
                {
                    await adapter.Module.InvokeVoidAsync("initializeControl", this.callContainer, adapter.Id, options);

                    this.lastControlOptions = options;
                }
            }
        }

        private bool ControlOptionsHasBeenChanged(CallControlOptions newOptions)
        {
            if (this.lastControlOptions is null)
            {
                return true;
            }

            if (this.lastControlOptions.CameraButton != newOptions.CameraButton)
            {
                return true;
            }

            if (this.lastControlOptions.DevicesButton != newOptions.DevicesButton)
            {
                return true;
            }

            if (this.lastControlOptions.EndCallButton != newOptions.EndCallButton)
            {
                return true;
            }

            if (this.lastControlOptions.MicrophoneButton != newOptions.MicrophoneButton)
            {
                return true;
            }

            if (this.lastControlOptions.MoreButton != newOptions.MoreButton)
            {
                return true;
            }

            if (this.lastControlOptions.ParticipantsButton != newOptions.ParticipantsButton)
            {
                return true;
            }

            if (this.lastControlOptions.PeopleButton != newOptions.PeopleButton)
            {
                return true;
            }

            if (this.lastControlOptions.RaiseHandButton != newOptions.RaiseHandButton)
            {
                return true;
            }

            if (this.lastControlOptions.ScreenShareButton != newOptions.ScreenShareButton)
            {
                return true;
            }

            return false;
        }
    }
}
