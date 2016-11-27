using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.Vehicles.Car;

public class game_controller_s : MonoBehaviour {

    public bool paused = true;
    public int laps_to_win = 1;
    public List<GameObject> winners = new List<GameObject>();
    private int place = 0;
    private bool begin = true;

    GameObject[] pauseObjects;
    public GameObject[] racers;
    public countdown_s countdown;

    void Start() {
        Time.timeScale = 0;
        pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
        showPaused();
        countdown.hideCountdown();
        TurnOffCars();
    }

    public void Finish(GameObject winner) {
        place++;
        winners.Add(winner);
        if(winner.tag == "Player") {
            if (place == 1) {
                Player_Win(place); 
            } else { 
                NPC_Win(place); 
            }
        }
    }

    public void Player_Win(int place) {
        "You Win!"
        "You came in " place "!"
    }

    public void NPC_Win(int place) {
        "Nice Try!"
        "You came in " place "!"

    }

    public void TogglePause() {
        paused = !paused;
        Debug.Log(paused);
        if (paused) {
            Time.timeScale = 0;
            showPaused();
        } else {
            Debug.Log("un paused");
            Time.timeScale = 1;
            hidePaused();
        }
    }

    //shows objects with ShowOnPause tag
    private void showPaused() {
        foreach (GameObject g in pauseObjects) {
            g.SetActive(true);
        }
    }

    //hides objects with ShowOnPause tag
    private void hidePaused() {
        foreach (GameObject g in pauseObjects) {
            g.SetActive(false);
        }
        // If first time continuing, show countdown.
        if (begin) {
            begin = false;
            showCountdown();
        }
    }

    private void showCountdown() {
        countdown.showCountdown();
        Invoke("TurnOnCars", countdown.time);
    }

    void TurnOnCars() {
        foreach (GameObject g in racers) {
            if (g.tag == "Player") {
                g.GetComponent<CarUserControl>().enabled = true;
            } else {
                g.GetComponent<CarAIControl>().enabled = true;
            }
        }
    }

    void TurnOffCars() {
        foreach (GameObject g in racers) {
            g.GetComponent<CarUserControl>().enabled = false;
            g.GetComponent<CarAIControl>().enabled = false;
        }
    }

}
