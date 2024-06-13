//-----------------------------------------------------------------------
// <copyright file="CallCompositePage.cs" company="P.O.S Informatique">
//     Copyright (c) P.O.S Informatique. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace PosInformatique.Azure.Communication.UI.Blazor
{
    /// <summary>
    /// Major UI screens shown in the <see cref="CallComposite"/>.
    /// </summary>
    public enum CallCompositePage
    {
        /// <summary>
        /// The teams meeting is denied.
        /// </summary>
        AccessDeniedTeamsMeeting,

        /// <summary>
        /// Call is ongoing.
        /// </summary>
        Call,

        /// <summary>
        /// Configuration step page.
        /// </summary>
        Configuration,

        /// <summary>
        /// The call is currently hold.
        /// </summary>
        Hold,

        /// <summary>
        /// The join call has been failed to network issues.
        /// </summary>
        JoinCallFailedDueToNoNetwork,

        /// <summary>
        /// The user has left the call.
        /// </summary>
        LeftCall,

        /// <summary>
        /// The user is currently leaving the call.
        /// </summary>
        Leaving,

        /// <summary>
        /// The user is waiting in the lobby.
        /// </summary>
        Lobby,

        /// <summary>
        /// The user has been removed from the call.
        /// </summary>
        RemovedFromCall,

        /// <summary>
        /// The <see cref="CallComposite"/> can not be loaded because the current environment is not supported.
        /// </summary>
        UnsupportedEnvironment,

        /// <summary>
        /// Transferring the current call.
        /// </summary>
        Transferring,
    }
}
