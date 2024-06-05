//-----------------------------------------------------------------------
// <copyright file="CommunicationUserKind.cs" company="P.O.S Informatique">
//     Copyright (c) P.O.S Informatique. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace PosInformatique.Azure.Communication.UI.Blazor
{
    /// <summary>
    /// Represents a standard user for Azure Communication Services.
    /// </summary>
    public class CommunicationUserKind : CommunicationUserIdentifier
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommunicationUserKind"/> class.
        /// </summary>
        /// <param name="communicationUserId">Id of the as returned from the Communication Service.</param>
        public CommunicationUserKind(string communicationUserId)
            : base(communicationUserId)
        {
        }
    }
}
