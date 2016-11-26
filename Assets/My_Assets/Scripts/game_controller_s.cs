using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class game_controller_s : MonoBehaviour {

    public bool paused = true;
    public int laps_to_win = 3;
    public List<GameObject> winners = new List<GameObject>();
    private int place = 0;
    private bool begin = true;

    GameObject[] pauseObjects;
    public countdown_s countdown;

    void Start() {
        Time.timeScale = 0;
        pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
        showPaused();
        countdown.hideCountdown();
    }

    public void Finish(GameObject winner) {
        place++;
        winners.Add(winner);
        if (place == 1) {
            if (winner.tag == "Player") { Player_Win(); } else { NPC_Win(); }
        }
    }

    public void Player_Win() {
    }

    public void NPC_Win() {
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
}
