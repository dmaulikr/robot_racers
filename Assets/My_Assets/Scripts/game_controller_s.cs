using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.Vehicles.Car;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class game_controller_s : MonoBehaviour {

    public bool paused = true;
    public int laps_to_win = 2;
    public List<GameObject> winners = new List<GameObject>();
    private int place = 0;
    //State Controllers
    private bool begin = true;
    private bool win_loss = false;

    GameObject[] pauseObjects;
    public GameObject[] racers;
    public countdown_s countdown;

    public GameObject win_screen;
    public GameObject loss_screen;
    public Text loss_place;

    void Start() {
        Time.timeScale = 0;
        pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
        showPaused();
        countdown.hideCountdown();
        win_screen.SetActive(false);
        loss_screen.SetActive(false);
        TurnOffCars();
    }

    public void Finish(GameObject winner) {
        place++;
        winners.Add(winner);
        if(winner.tag == "Player") {
            if (place == 1) {
                print("Player Wins!");
                Player_Win(place); 
            } else {
                print("NPC Wins!");
                NPC_Win(place); 
            }
        }
    }

    public void Player_Win(int place) {
        win_screen.SetActive(true);
        win_loss = true;
    }

    public void NPC_Win(int place) {
        loss_screen.SetActive(true);
        win_loss = true;
        //Determine the ending for the place
        string end_for_place;
        if(place == 2) {
            end_for_place = "nd";
        } else if (place == 3) {
            end_for_place = "rd";
        } else {
            end_for_place = "th";
        }
        loss_place.text = "You came in " + place + end_for_place + " place!";
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

    // Restart level if Space is pressed in the win or loss screen
    void Update() {
        if (win_loss && Input.GetKeyUp(KeyCode.Space)) {
            //Application.LoadLevel(Application.loadedLevel); Apparently obsolete?
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
