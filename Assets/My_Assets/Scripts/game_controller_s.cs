using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class game_controller_s : MonoBehaviour {

    public int laps_to_win = 3;
    public List<GameObject> winners = new List<GameObject>();
    private int place = 0;

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
}
