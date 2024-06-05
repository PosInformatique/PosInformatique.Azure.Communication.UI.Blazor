import {
    AzureCommunicationTokenCredential,
    CallComposite,
    createAzureCommunicationCallAdapter,
    createRoot,
    createElement,
    initializeIcons,
} from '/_content/PosInformatique.Azure.Communication.UI.Blazor/azure-communication-react-bundle.js'

initializeIcons(undefined, { disableWarnings: true });

export async function createCallAdapter(referenceId, args, eventCallback) {

    var adapter = await createAzureCommunicationCallAdapter({
        userId: args.userId,
        displayName: args.displayName,
        credential: new AzureCommunicationTokenCredential(args.credential.token),
        locator: args.locator,
        options: args.options
    });


    adapter.on('callEnded', (event) => {
        return eventCallback.invokeMethodAsync('OnCallEndedAsync', event);
    });

    adapter.on('participantsJoined', (event) => {
        return eventCallback.invokeMethodAsync('OnParticipantsJoinedAsync', event.joined.map(createRemoteParticipant));
    });

    adapter.on('participantsLeft', (event) => {
        return eventCallback.invokeMethodAsync('OnParticipantsLeftAsync', event.removed.map(createRemoteParticipant));
    });

    registerAdapter(referenceId, adapter);
}

export function initializeControl(referenceId, divElement) {

    var adapter = getAdapter(referenceId);

    if (typeof divElement.adapter != "undefined") {
        if (divElement.adapter == adapter) {
            return;
        }

        dispose(divElement);
    }

    divElement.adapter = getAdapter(referenceId);

    createRoot(divElement).render(createElement(CallComposite, { adapter: divElement.adapter }, null));
}

export function adapterJoinCall(referenceId, options) {

    const adapter = getAdapter(referenceId);

    adapter.joinCall(options);
}

export function dispose(referenceId) {

    const adapter = getAdapter(referenceId);

    if (divElement.adapter != null) {
        divElement.adapter = null;
    }

    adapter.dispose();

    delete window.__posInfo_azure_comm_ui_blazor[referenceId];
}

function getAdapter(referenceId) {
    return window.__posInfo_azure_comm_ui_blazor[referenceId];
}

function registerAdapter(referenceId, adapter) {

    if (typeof window.__posInfo_azure_comm_ui_blazor == "undefined") {
        window.__posInfo_azure_comm_ui_blazor = {};
    }

    window.__posInfo_azure_comm_ui_blazor[referenceId] = adapter;
}

function createRemoteParticipant(remoteParticipant) {
    return {
        identifier: remoteParticipant.identifier,
        displayName: remoteParticipant.displayName,
    };
}