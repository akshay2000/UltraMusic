function state() {
    button = document.getElementsByClassName("playbackControls")[0].children[1]
    if (button.classList.contains("disabled")) {
        return "idle";
    } else {
        if (getComputedStyle(button, ":before").content == '"\ue60a"') {
            return "paused";
        } else if (getComputedStyle(button, ":before").content == '"\ue609"') {
            return "playing";
        } else {
            return "idle";
        }
    }
}
state();