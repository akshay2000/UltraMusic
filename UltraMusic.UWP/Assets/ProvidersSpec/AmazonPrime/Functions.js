function observePlaybackState() {
    if (document.isObservingPlaybackState)
        return;

    var targetNode = document.getElementsByClassName("playbackControls")[0]
    if (!targetNode)
        return;

    var observerConfig = { childList: true, subtree: true };

    var playbackStateChanged = function (mutations, observer) {
        window.external.notify("PlaybackStateChanged");
    };
    var playbackStateObserver = new MutationObserver(playbackStateChanged);
    playbackStateObserver.observe(targetNode, observerConfig);
    document.isObservingPlaybackState = true;
}

function addObservers() {
    observePlaybackState();
}

function getPlayerState() {
    let button = document.getElementsByClassName("playbackControls")[0].children[1]
    if (button.classList.contains("disabled")) {
        return "idle";
    } else {
        let buttonContent = getComputedStyle(button, ":before").content
        if (buttonContent == '"\ue60a"' || buttonContent == "\ue60a") {
            return "paused";
        } else if (buttonContent == '"\ue609"' || buttonContent == "\ue609") {
            return "playing";
        } else {
            return "idle";
        }
    }
}

function previous() {
    document.getElementsByClassName("playbackControls")[0].children[0].click()
}

function next() {
    document.getElementsByClassName("playbackControls")[0].children[2].click();
}

function pause() {
    let state = getPlayerState();
    if (state != "paused") {
        document.getElementsByClassName('playbackControls')[0].children[1].click();
    }
}

function play() {
    let state = getPlayerState();
    if (state != "playing") {
        document.getElementsByClassName('playbackControls')[0].children[1].click();
    }
}

areUMFunctionsAvailable = "true";