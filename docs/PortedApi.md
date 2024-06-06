## Ported API

This section contains the list of the APIs from the
[Adapters for Composites documentation](https://azure.github.io/communication-ui-library/?path=/docs/composite-adapters--page)
which has been ported to this library.

### CallAdapter

#### Methods

| Method                        | Available  | Remarks                                              |
|-------------------------------|------------|------------------------------------------------------|
| onStateChange                 | TODO       |                                                      |
| offStateChange                | TODO       |                                                      |
| getState                      | TODO       |                                                      |
| dispose                       | **Done**   |                                                      |
| holdCall (Beta)               | No         | Currently in beta                                    |
| joinCall (Deprecated)         | No         | Deprecated                                           |
| joinCall                      | Partially  | Need to wrap the Call returned object                |
| resumeCall (Beta)             | No         | Currently in beta                                    |
| startCamera                   | TODO       |                                                      |
| stopCamera                    | TODO       |                                                      |
| mute                          | **Done**   |                                                      |
| unmute                        | **Done**   |                                                      |
| startCall (Beta)              | No         | Currently in beta                                    |
| startScreenShare              | **Done**   |                                                      |
| stopScreenShare               | **Done**   |                                                      |
| addParticipant (Beta)         | No         | Currently in beta                                    |
| removeParticipant             | TODO       |                                                      |
| createStreamView              | TODO       |                                                      |
| disposeStreamView             | TODO       |                                                      |
| askDevicePermission           | TODO       |                                                      |
| queryCameras                  | TODO       |                                                      |
| queryMicrophones              | TODO       |                                                      |
| querySpeakers                 | TODO       |                                                      |
| setCamera                     | TODO       |                                                      |
| setMicrophone                 | TODO       |                                                      |
| setSpeaker                    | TODO       |                                                      |
| startCaptions                 | TODO       |                                                      |
| stopCaptions                  | TODO       |                                                      |
| raiseHand                     | TODO       |                                                      |
| lowerHand                     | TODO       |                                                      |
| setCaptionLanguage            | TODO       |                                                      |
| setSpokenLanguage             | TODO       |                                                      |
| submitSurvey                  | TODO       |                                                      |
| startVideoBackgroundEffect    | TODO       |                                                      |
| stopVideoBackgroundEffects    | TODO       |                                                      |
| updateBackgroundPickerImages  | TODO       |                                                      |
| updateSelectedVideoBackgroundEffect | TODO  |                                                      |


#### Events
| Name                              | Available | Remarks |
|-----------------------------------|-----------|---------|
| participantsJoined                | **Done**  |         |
| participantsLeft                  | **Done**  |         |
| isMutedChanged                    | TODO      |         |
| callIdChanged                     | TODO      |         |
| isLocalScreenSharingActiveChanged | TODO      |         |
| displayNameChanged                | TODO      |         |
| isSpeakingChanged                 | TODO      |         |
| callEnded                         | **Done**  |         |
| diagnosticChanged                 | TODO      |         |
| error                             | TODO      |         |
| captionsReceived                  | TODO      |         |
| isCaptionsActiveChanged           | TODO      |         |
| transferAccepted                  | TODO      |         |
| capabilitiesChanged               | TODO      |         |
| spotlightChanged                  | TODO      |         |
