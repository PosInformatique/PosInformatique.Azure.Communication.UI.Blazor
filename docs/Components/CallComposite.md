## CallComposite

The `CallComposite` provides a calling experience that allows users to start or join a call.
Inside the experience users can configure their devices, participate in the call with video and see other participants,
including those with video turned on.
This component is a wrapper of the
[JavaScript CallComposite component](https://azure.github.io/communication-ui-library/?path=/docs/composites-call-basicexample--basic-example)
library.

To use the component:
- Add the `CallComposite` component.
- Define a reference to the component which allows to call the `LoadAsync()`
method to load the component with the token and ID of the Azure Communication Services to use.

Example:
```razor
<CallComposite @ref="this.callComposite"
               OnCallEnded="this.OnCallEnded"
               OnParticipantJoined="this.OnParticipantJoined"
               OnParticipantLeft="this.OnParticipantLeft">
</CallComposite>

<button @onclick="this.LoadAsync">Load</button>

@{
    private CallComposite? callComposite;

    private async Task LoadAsync()
    {
        var args = new CallAdapterArgs(
            new UserIdentifier("...Communication Identifier..."),
            new GroupCallLocator("Group ID"),
            new TokenCredential("The ACS token"))
            {
                DisplayName = "John doe",
                Options =
                {
                    CallControls =
                    {
                        CameraButton = true,
                        DevicesButton = true,
                        EndCallButton = true,
                        MicrophoneButton = true,
                        MoreButton = true,
                        ParticipantsButton = true,
                        PeopleButton = true,
                        RaiseHandButton = true,
                        ScreenShareButton = true,
                    }
                }
            };

        await this.callComposite!.LoadAsync(args);
    }
}
```

### Join the call
After the component has been loaded (or after leaving a call), it is possible to join the call
by calling the `JoinCall()` method. You can define if the camera and/or the microphone have to be
activated.

```csharp
private async Task JoinCallAsync()
{
    var options = new JoinCallOptions()
    {
        CameraOn = true,
        MicrophoneOn = true,
    };

    await this.callComposite!.JoinCallAsync(options);
}
```

### Events
You can subsribe to the following events:
- `OnCallEnded`: Occurs then the call is ended.
- `OnParticipantJoined`: Occurs when a participant join the call.
- `OnParticipantLeft`: Occurs when a participant leave the call.