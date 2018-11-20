function state() {
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
state();
