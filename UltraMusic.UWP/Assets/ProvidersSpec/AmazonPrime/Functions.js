function buildObserver(targetNode, notification) {
    let observerConfig = { childList: true, subtree: true };

    let callback = function (mutations, observer) {
        window.external.notify(notification);
    };
    var observer = new MutationObserver(callback);
    observer.observe(targetNode, observerConfig);
}

function observePlaybackState() {
    if (document.isObservingPlaybackState)
        return;

    var targetNode = document.getElementsByClassName("playbackControls")[0]
    if (!targetNode)
        return;
    buildObserver(targetNode, "PlaybackStateChanged");
    
    document.isObservingPlaybackState = true;
}

function observeNowPlaying() {
    if (document.isObservingNowPlaying)
        return;

    var targetNode = document.getElementsByClassName("trackInfoContainer")[0]
    if (!targetNode)
        return;
    buildObserver(targetNode, "NowPlayingChanged");

    document.isObservingNowPlaying = true;
}

function addObservers() {
    observePlaybackState();
    observeNowPlaying();
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

// region Playback Controls

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

// endregion

function getNowPlaying() {
    let albumArt = document.getElementsByClassName("playbackControlsView")[0].getElementsByClassName("renderImage")[0].src;
    let title = document.getElementsByClassName("trackTitleWrapper")[0].getElementsByClassName("trackTitle")[0].children[0].title;
    let artist = document.getElementsByClassName("trackInfoWrapper")[0].getElementsByClassName("trackArtist")[0].children[0].title;
    let album = document.getElementsByClassName("trackInfoWrapper")[0].getElementsByClassName("trackSourceLink")[0].children[0].children[0].title;
    let ret = {
        albumArt: albumArt,
        title: title,
        artist: artist,
        album: album
    };
    return JSON.stringify(ret);
}

function search(query) {
    s = document.getElementById("searchMusic");
    s.value = query;
    document.getElementById("dragonflyTransport").getElementsByClassName("playerIconSearch")[0].click();
    return "true";
}

areUMFunctionsAvailable = "true";