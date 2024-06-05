﻿//-----------------------------------------------------------------------
// <copyright file="CommunicationUserIdentifier.cs" company="P.O.S Informatique">
//     Copyright (c) P.O.S Informatique. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace PosInformatique.Azure.Communication.UI.Blazor
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// Represents an Azure Communication user.
    /// </summary>
    [JsonDerivedType(typeof(CommunicationUserKind), "communicationUser")]
    [JsonPolymorphic(TypeDiscriminatorPropertyName = "kind")]
    public abstract class CommunicationUserIdentifier
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommunicationUserIdentifier"/> class.
        /// </summary>
        /// <param name="communicationUserId">Id of the CommunicationUser as returned from the Communication Service.</param>
        protected CommunicationUserIdentifier(string communicationUserId)
        {
            this.CommunicationUserId = communicationUserId;
        }

        /// <summary>
        /// Gets the id of the CommunicationUser as returned from the Communication Service.
        /// </summary>
        [JsonPropertyName("communicationUserId")]
        [JsonPropertyOrder(1)]
        public string CommunicationUserId { get; }
    }
}
