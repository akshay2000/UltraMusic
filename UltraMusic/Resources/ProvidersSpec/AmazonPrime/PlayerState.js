function state() {
    button = document.getElementsByClassName("playbackControls")[0].children[1]
    if (button.classList.contains("disabled")) {
        return "idle";
    } else {
        if (getComputedStyle(button, ":before").content == "\"\ue60a\"") {
            return "paused";
        } else {
            return "playing";
        }
    }
}
state();