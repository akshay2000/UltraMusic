function observePlaybackState() {
    if (document.isObservingPlaybackState)
        return;

    var targetNode = document.getElementsByClassName("playbackControls")[0]
    if (!targetNode)
        return;

    var observerConfig = { childList: true, subtree: true };

    var playbackStateChanged = function (mutations, observer) {
        window.webkit.messageHandlers.playbackState.postMessage("AmazonPrime");
    };
    var playbackStateObserver = new MutationObserver(playbackStateChanged);
    playbackStateObserver.observe(targetNode, observerConfig);
    document.isObservingPlaybackState = true;
}

function addObservers() {
    observePlaybackState();
}