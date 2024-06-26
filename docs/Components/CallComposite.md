## CallComposite

The `CallComposite` provides a calling experience that allows users to start or join a call.
Inside the experience users can configure their devices, participate in the call with video and see other participants,
including those with video turned on.
This component is a wrapper of the
[JavaScript CallComposite component](https://azure.github.io/communication-ui-library/?path=/docs/composites-call-basicexample--basic-example)
library.

To use the component:
- Register required services by calling the `AddCalling()` method in the main entry of the Blazor application.

```csharp
builder.Services.AddCalling();
```

- Inject the `ICallingService` dependency a use it to create an instance of `ICallAdapter`.
- Add the `CallComposite` component and bind the `Adapter` property with the `ICallAdapter` previously created.

Example:
```razor
@inject ICallingService CallingService

<CallComposite Adapter="this.callAdapter"
               CameraButton="true"
               DevicesButton="true"
               EndCallButton="true"
               MicrophoneButton="true"
               MoreButton="@this.moreButton"
               ParticipantsButton="true"
               PeopleButton="true"
               RaiseHandButton="true"
               ScreenShareButton="true" />

<button @onclick="this.LoadAsync">Load</button>

@code
{
    private ICallAdapter? callAdapter;

    private async Task LoadAsync()
    {
        var args = new CallAdapterArgs(
            new UserIdentifier("...Communication Identifier..."),
            new GroupCallLocator("Group ID"),
            new TokenCredential("The ACS token"))
            {
                DisplayName = "John doe",
            };

        this.callAdapter = await this.CallingService.CreateAdapterAsync(args);

        this.callAdapter.OnCallEnded += this.OnCallEnded;
        this.callAdapter.OnParticipantJoined += this.OnParticipantJoined;
        this.callAdapter.OnParticipantLeft += this.OnParticipantLeft;
    }
}
```

You can manage the `CallComposite` component using the `ICallAdapter` associated. For example, you can
subscribe to different events using a simple delegate.

### Join/Leave the call
After the `ICallAdapter` has been associated to the `CallComposite` component
(or after leaving a call), it is possible to join the call
by calling the `JoinCall()` method on the `ICallAdapter`.
You can define if the camera and/or the microphone have to be activated.

```csharp
private async Task JoinCallAsync()
{
    var options = new JoinCallOptions()
    {
        CameraOn = true,
        MicrophoneOn = true,
    };

    await this.callAdapter!.JoinCallAsync(options);
}
```

To leave the call, call the `LeaveCallAsync()` method on the `ICallAdapter`. This method
take a boolean parameter `forEveryone` to remove all participants when leaving.

### Start/Stop screen share
To start sharing the screen on the current device, call the `StartScreenShare()` method on the `ICallAdapter`.

To stop sharing the screen on the current device, call the `StopScreenShare()` method on the `ICallAdapter`.

### Mute/Unmute
To mute the microphone of the current user, call the `MuteAsync()` method on the `ICallAdapter`.

To unmute the microphone of the current user, call the `UnmuteAsync()` method on the `ICallAdapter`.

### Events
You can subsribe to the following asynchronous events using a standard delegate method:
- `OnCallEnded`: Occurs then the call is ended.
- `OnMicrophoneMuteChanged`: Occurs when the microphone of a participant is mute/unmute.
- `OnParticipantJoined`: Occurs when a participant join the call.
- `OnParticipantLeft`: Occurs when a participant leave the call.

### Dispose the resources
It is recommanded to implement the `IAsyncDisposable` method in the class which create
and manage the `ICallAdapter` instance.

### Unit tests
The `ICallingService.CreateAdapterAsync()` method returns an instance of `ICallAdapter`
implemented by the `CallAdapter`. By returning interface implementation, developers
have no excuses to perform some units in their code by mocking the `ICallingService`
and `ICallAdapter` interfaces.