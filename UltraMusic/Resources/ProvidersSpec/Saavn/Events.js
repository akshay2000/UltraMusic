function observePlaybackState() {
    if (document.isObservingPlaybackState)
        return;

    var targetNode = document.getElementById("controls")
    if (!targetNode)
        return;

    var observerConfig = { attributes: true, childList: true, subtree: true };

    var playbackStateChanged = function (mutations, observer) {
        window.webkit.messageHandlers.playbackState.postMessage("Saavn");
    };
    var playbackStateObserver = new MutationObserver(playbackStateChanged);
    playbackStateObserver.observe(targetNode, observerConfig);
    document.isObservingPlaybackState = true;
}

function addObservers() {
    observePlaybackState();
}