using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityStandardAssets.Vehicles.Car;
using UnityStandardAssets.Utility;

public class lap_controller_s : MonoBehaviour {

    private int lap = 0;
    private int check = 0;
    public game_controller_s the_game_controller;
    public Text lap_display;

    private CarAIControl my_ai;
    private CarUserControl my_control;
    private pause_controller_s my_pause;
    private WaypointProgressTracker my_wpt;

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
        my_ai = GetComponent<CarAIControl>();
        my_control = GetComponent<CarUserControl>();
        my_pause = GetComponent<pause_controller_s>();
        my_wpt = GetComponent<WaypointProgressTracker>();

        SetComponents();
        Update_Lap_Display();
    }

    void SetComponents() {
        if (tag == "Player") {
            my_ai.enabled = false;
            my_wpt.enabled = false;
        } else {
            my_pause.enabled = false;
            my_control.enabled = false;
        }
    }

    void Update_Lap_Display() {
        if (gameObject.tag == "Player") {
            lap_display.text = "Laps: " + lap.ToString() + "/" + the_game_controller.laps_to_win.ToString();
        }
    }
}
