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
        private ElementReference callContainer;

        /// <summary>
        /// Gets or sets the <see cref="CallAdapter"/> which provides the logic and data of the composite control.
        /// The <see cref="CallComposite"/> can also be controlled using the adapter.
        /// </summary>
        [Parameter]
        [EditorRequired]
        public CallAdapter? Adapter { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to
        /// show or hide Camera Button during a call.
        /// Default value: <see cref="true"/>.
        /// </summary>
        [Parameter]
        public bool CameraButton { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to
        /// show or hide Devices button during a call.
        /// Default value: <see cref="true"/>.
        /// </summary>
        [Parameter]
        public bool DevicesButton { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to
        /// show or hide EndCall button during a call.
        /// Default value: <see cref="true"/>.
        /// </summary>
        [Parameter]
        public bool EndCallButton { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to
        /// show or hide Microphone button during a call.
        /// Default value: <see cref="true"/>.
        /// </summary>
        [Parameter]
        public bool MicrophoneButton { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to
        /// show, hide or disable the more button during a call.
        /// Default value: <see cref="true"/>.
        /// </summary>
        [Parameter]
        public bool MoreButton { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to
        /// show, hide or disable participants button during a call.
        /// Default value: <see cref="true"/>.
        /// </summary>
        [Parameter]
        public bool ParticipantsButton { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to
        /// show, hide or disable the people button during a call.
        /// Default value: <see cref="true"/>.
        /// </summary>
        [Parameter]
        public bool PeopleButton { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to
        /// show, hide or disable the raise hand button during a call.
        /// Default value: <see cref="true"/>.
        /// </summary>
        [Parameter]
        public bool RaiseHandButton { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to
        /// show, hide or disable the screen share button during a call.
        /// Default value: <see cref="true"/>.
        /// </summary>
        [Parameter]
        public bool ScreenShareButton { get; set; }

        /// <inheritdoc />
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (this.Adapter is not null)
            {
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

                await this.Adapter.Module.InvokeVoidAsync("initializeControl", this.callContainer, this.Adapter.Id, options);
            }
        }
    }
}
