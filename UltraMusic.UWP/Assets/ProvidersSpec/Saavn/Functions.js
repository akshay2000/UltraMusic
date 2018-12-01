function observePlaybackState() {
    if (document.isObservingPlaybackState)
        return;

    var targetNode = document.getElementById("controls");
    console.log(targetNode);
    if (!targetNode)
        return;

    var observerConfig = { attributes: true, childList: true, subtree: true };

    var playbackStateChanged = function (mutations, observer) {
        window.external.notify("PlaybackStateChanged");
    };
    var playbackStateObserver = new MutationObserver(playbackStateChanged);
    playbackStateObserver.observe(targetNode, observerConfig);
    document.isObservingPlaybackState = true;
}

function getPlayerState() {
    if (document.getElementById("now-playing").getElementsByClassName("key-art")[0].style.backgroundImage == "none") {
        return "idle";
    } else {
        if (document.getElementById("play").classList.contains("hide")) {
            return "playing";
        } else {
            return "paused";
        }
    }
}

function addObservers() {
    observePlaybackState();
}


function previous() {
    document.getElementById("rew").click();
}

function next() {
    document.getElementById("fwd").click();
}

function pause() {
    let state = getPlayerState();
    if (state != "paused") {
        document.getElementById("pause").click();
    }
}

function play() {
    let state = getPlayerState();
    if (state != "playing") {
        document.getElementById("play").click();
    }
}

areUMFunctionsAvailable = "true";