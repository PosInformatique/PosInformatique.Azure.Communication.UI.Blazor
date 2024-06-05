//-----------------------------------------------------------------------
// <copyright file="CallComposite.razor.cs" company="P.O.S Informatique">
//     Copyright (c) P.O.S Informatique. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace PosInformatique.Azure.Communication.UI.Blazor
{
    using Microsoft.AspNetCore.Components;

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

        /// <inheritdoc />
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (this.Adapter is not null)
            {
                await this.Adapter.InitializeControlAsync(this.callContainer);
            }
        }
    }
}
