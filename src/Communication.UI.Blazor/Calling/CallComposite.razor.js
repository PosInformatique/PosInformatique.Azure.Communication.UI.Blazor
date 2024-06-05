import {
    AzureCommunicationTokenCredential,
    CallComposite,
    createAzureCommunicationCallAdapter,
    createRoot,
    createElement,
    initializeIcons,
} from '/_content/PosInformatique.Azure.Communication.UI.Blazor/azure-communication-react-bundle.js'

export async function initialize(divElement, args, eventCallback) {

    initializeIcons(undefined, { disableWarnings: true });

    divElement.adapter = await createAzureCommunicationCallAdapter({
        userId: args.userId,
        displayName: args.displayName,
        credential: new AzureCommunicationTokenCredential(args.credential.token),
        locator: args.locator,
        options: args.options
    });

    createRoot(divElement).render(createElement(CallComposite, { ...args, adapter: divElement.adapter }, null));

    divElement.adapter.on('callEnded', (event) => {
        return eventCallback.invokeMethodAsync('OnCallEndedAsync', event);
    });

    divElement.adapter.on('participantsJoined', (event) => {
        return eventCallback.invokeMethodAsync('OnParticipantsJoinedAsync', event.joined.map(createRemoteParticipant));
    });

    divElement.adapter.on('participantsLeft', (event) => {
        return eventCallback.invokeMethodAsync('OnParticipantsLeftAsync', event.removed.map(createRemoteParticipant));
    });
}

export function adapterJoinCall(divElement, options) {
    divElement.adapter.joinCall(options);
}

export function dispose(divElement) {
    if (divElement.adapter != null) {
        divElement.adapter.dispose();
        divElement.adapter = null;
    }
}

function createRemoteParticipant(remoteParticipant) {
    return {
        identifier: remoteParticipant.identifier,
        displayName: remoteParticipant.displayName,
    };
}