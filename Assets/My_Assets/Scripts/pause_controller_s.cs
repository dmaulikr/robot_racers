using UnityEngine;
using System.Collections;

public class pause_controller_s : MonoBehaviour {
    public game_controller_s the_game_controller;

    void Update() {
        if (the_game_controller.paused) {
            if (Input.anyKey) {
                the_game_controller.TogglePause();
            }
        } else {
            //uses the escape button to pause and unpause the game
            if (Input.GetKeyUp(KeyCode.Escape)) {
                the_game_controller.TogglePause();
            }
        }
    }
}

