function observePlaybackState() {
    if (document.isObservingPlaybackState)
        return;

    var targetNode = document.getElementsByClassName("material-player-middle");
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
    if (document.getElementById("playerSongInfo").style.display == "none") {
        return "idle";
    } else {
        if (document.getElementById("player-bar-play-pause").classList.contains("playing")) {
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
    document.getElementById("player-bar-rewind").click();
}

function next(){
    document.getElementById("player-bar-forward").click();
}

function pause() {
    let state = getPlayerState();
    if (state != "paused") {
        document.getElementById("player-bar-play-pause").click();
    }
}

function play() {
    let state = getPlayerState();
    if (state != "playing") {
        document.getElementById("player-bar-play-pause").click()
    }
}

function getNowPlaying() {
    let albumArt = document.getElementById("playerBarArt").src;
    let title = document.getElementById("currently-playing-title").title;
    let artist = document.getElementById("player-artist").childNodes[0].data;
    let album = document.getElementsByClassName("player-album")[0].childNodes[0].data;
    let ret = {
        albumArt: albumArt,
        title: title,
        artist: artist,
        album: album
    };
    return JSON.stringify(ret);
}

areUMFunctionsAvailable = "true";
