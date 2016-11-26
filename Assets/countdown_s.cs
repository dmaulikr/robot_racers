using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class countdown_s : MonoBehaviour {
    public GameObject three;
    public GameObject two;
    public GameObject one;
    public GameObject go;

    public float time = 4f;

    public void hideCountdown() {
        three.SetActive(false);
        two.SetActive(false);
        one.SetActive(false);
        go.SetActive(false);
    }
        
    public void showCountdown() {
        three.SetActive(true);
        Invoke("show_two", 1f);
    }

    void show_two() {
        three.SetActive(false);
        two.SetActive(true);
        Invoke("show_one", 1f);
    }
    
    void show_one() {
        two.SetActive(false);
        one.SetActive(true);
        Invoke("show_go", 1f);
    }

    void show_go() {
        one.SetActive(false);
        go.SetActive(true);
        Invoke("hide_go", 1f);
    }

    void hide_go() {
        go.SetActive(false);
    }
}
