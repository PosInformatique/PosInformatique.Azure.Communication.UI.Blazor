import {
    AzureCommunicationTokenCredential,
    CallComposite,
    createAzureCommunicationCallAdapter,
    createRoot,
    createElement,
    initializeIcons,
} from '/_content/PosInformatique.Azure.Communication.UI.Blazor/azure-communication-react-bundle.js'

initializeIcons(undefined, { disableWarnings: true });

export async function createCallAdapter(id, args, eventCallback) {

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

    adapter.on('isMutedChanged', (event) => {
        return eventCallback.invokeMethodAsync('OnMicrophoneMuteChangedAsync', event);
    });

    adapter.on('participantsJoined', (event) => {
        return eventCallback.invokeMethodAsync('OnParticipantsJoinedAsync', event.joined.map(createRemoteParticipant));
    });

    adapter.on('participantsLeft', (event) => {
        return eventCallback.invokeMethodAsync('OnParticipantsLeftAsync', event.removed.map(createRemoteParticipant));
    });

    adapter.onStateChange((state) => {
        console.log(state);
        return eventCallback.invokeMethodAsync('OnStateChangedAsync', createState(state));
    });

    registerAdapter(id, adapter);
}

export function initializeControl(divElement, adapterId, callControls) {

    var adapter = getAdapter(adapterId);

    var element = createElement(CallComposite, { options: { callControls: callControls }, adapter: adapter }, null);

    createRoot(divElement).render(element);
}

export function adapterGetState(id) {

    const adapter = getAdapter(id);

    return createState(adapter.getState());
}

export function adapterJoinCall(id, options) {

    const adapter = getAdapter(id);

    adapter.joinCall(options);
}

export async function adapterLeaveCall(id, forEveryone) {

    const adapter = getAdapter(id);

    await adapter.leaveCall(forEveryone);
}

export async function adapterLowerHand(id) {

    const adapter = getAdapter(id);

    await adapter.lowerHand();
}

export async function adapterMute(id) {

    const adapter = getAdapter(id);

    await adapter.mute();
}

export async function adapterQueryCameras(id) {

    const adapter = getAdapter(id);

    var cameras = await adapter.queryCameras();

    return cameras.map(createVideoDevice)
}

export async function adapterQueryMicrophones(id) {

    const adapter = getAdapter(id);

    var cameras = await adapter.queryMicrophones();

    return cameras.map(createAudioDevice)
}

export async function adapterQuerySpeakers(id) {

    const adapter = getAdapter(id);

    var cameras = await adapter.querySpeakers();

    return cameras.map(createAudioDevice)
}

export async function adapterRaiseHand(id) {

    const adapter = getAdapter(id);

    await adapter.raiseHand();
}

export async function adapterStartScreenShare(id) {

    const adapter = getAdapter(id);

    await adapter.startScreenShare();
}

export async function adapterStopScreenShare(id) {

    const adapter = getAdapter(id);

    await adapter.stopScreenShare();
}

export async function adapterUnmute(id) {

    const adapter = getAdapter(id);

    await adapter.unmute();
}

export function dispose(id) {

    const adapter = getAdapter(id);

    adapter.dispose();

    delete window.__posInfo_azure_comm_ui_blazor[id];
}

function getAdapter(id) {
    return window.__posInfo_azure_comm_ui_blazor[id];
}

function registerAdapter(id, adapter) {

    if (typeof window.__posInfo_azure_comm_ui_blazor == "undefined") {
        window.__posInfo_azure_comm_ui_blazor = {};
    }

    window.__posInfo_azure_comm_ui_blazor[id] = adapter;
}

function createAudioDevice(audioDevice) {
    return {
        name: audioDevice.name,
        id: audioDevice.id,
        isSystemDefault: audioDevice.isSystemDefault,
        deviceType: audioDevice.deviceType,
    };
}

function createRemoteParticipant(remoteParticipant) {
    return {
        identifier: remoteParticipant.identifier,
        displayName: remoteParticipant.displayName,
    };
}

function createState(state) {
    return {
        cameraStatus: state.cameraStatus,
        displayName: state.displayName,
        isLocalPreviewMicrophoneEnabled: state.isLocalPreviewMicrophoneEnabled,
        isRoomsCall: state.isRoomsCall,
        isTeamsCall: state.isTeamsCall,
        page: state.page,
        userId: state.userId,
    };
}

function createVideoDevice(videoDevice) {
    return {
        name: videoDevice.name,
        id: videoDevice.id,
        deviceType: videoDevice.deviceType,
    };
}