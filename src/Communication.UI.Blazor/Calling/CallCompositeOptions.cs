//-----------------------------------------------------------------------
// <copyright file="CallCompositeOptions.cs" company="P.O.S Informatique">
//     Copyright (c) P.O.S Informatique. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace PosInformatique.Azure.Communication.UI.Blazor
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// Optional features of the <see cref="CallComposite"/>.
    /// </summary>
    public class CallCompositeOptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CallCompositeOptions"/> class.
        /// </summary>
        public CallCompositeOptions()
        {
            this.CallControls = new CallControlOptions();
        }

        /// <summary>
        /// Gets the customization options for the control bar in calling experience.
        /// </summary>
        [JsonPropertyOrder(1)]
        [JsonPropertyName("callControls")]
        public CallControlOptions CallControls { get; }
    }
}
