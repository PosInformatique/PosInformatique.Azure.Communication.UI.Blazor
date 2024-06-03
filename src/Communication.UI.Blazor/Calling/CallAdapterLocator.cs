//-----------------------------------------------------------------------
// <copyright file="CallAdapterLocator.cs" company="P.O.S Informatique">
//     Copyright (c) P.O.S Informatique. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace PosInformatique.Azure.Communication.UI.Blazor
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// Locator used by call adapter to locate the call to join.
    /// </summary>
    [JsonDerivedType(typeof(GroupCallLocator), "groupCall")]
    [JsonDerivedType(typeof(CallParticipantsLocator), "callParticipants")]
    [JsonPolymorphic(TypeDiscriminatorPropertyName = "kind")]
    public abstract class CallAdapterLocator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CallAdapterLocator"/> class.
        /// </summary>
        protected CallAdapterLocator()
        {
        }
    }
}
