using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class lap_controller_s : MonoBehaviour {

    private int lap = 0;
    private int check = 0;
    public game_controller_s the_game_controller;
    public Text lap_display;

    void OnTriggerEnter(Collider col) {
        if(col.gameObject.name == "FinishLine" && check == lap) {
            lap++;
        } else if (col.gameObject.name == "HalfLine" && check == lap - 1) {
            check++;
        }

        if (lap > 3)
            the_game_controller.Finish(gameObject);

        Update_Lap_Display();
    }

    void Start() {
        Update_Lap_Display();
    }

    void Update_Lap_Display() {
        lap_display.text = "Laps: " + lap.ToString() + "/" + the_game_controller.laps_to_win.ToString();
    }

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
