var targetNode = document.getElementsByClassName("playbackControls")[0]
var observerConfig = { attributes: true, childList: true, subtree: true };

var playbackStateChanged = function(mutations, observer) {
    window.webkit.messageHandlers.playbackState.postMessage(JSON.stringify(mutations));
};
var playbackStateObserver = new MutationObserver(playbackStateChanged);
playbackStateObserver.observe(targetNode, observerConfig);