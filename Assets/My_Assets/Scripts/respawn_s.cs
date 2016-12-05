using UnityEngine;
using System.Collections;
using UnityStandardAssets.Utility;

public class respawn_s : MonoBehaviour {

    private Rigidbody rb;
    //public WaypointList waypointList = new WaypointList();
    public WaypointCircuit circuit;

    // For checking if car hasn't moved lately
    public float time_to_wait = 5f;
    public float move_threshold = 0.5f;
    private Vector3 current_pos;
    private Rigidbody m_Rigidbody;

    public GameObject normal_robot;
    public GameObject ragdoll_robot;
    public GameObject ragdoll_prefab;

    private bool allow_jettison = true;

	// Use this for initialization
	void Awake () {
        rb = GetComponent<Rigidbody>();
	}

    void Start() {
        m_Rigidbody = GetComponent<Rigidbody>();
        current_pos = transform.position;
        if (gameObject.tag == "NPC") {
            Invoke("Check_Pos", time_to_wait*5);
        }
    }

    void OnTriggerEnter(Collider col) {
        //print("Collision!");
        if (col.gameObject.tag == "Kill") {
            //print("Tag: Kill!");
            Jettison();
        }
    }

    void Respawn() {
        //print("Respawn");
        int idx;
        // Set velocity to 0
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        // Get closest respawn point (which are the waypoints)
        Transform respawn_point = GetClosestWaypoint(circuit.Waypoints, out idx);
        // Place ourselves at the waypoints position
        transform.position = respawn_point.position;
        //Make sure we loop back
        if(idx >= circuit.Waypoints.Length - 1) idx = -1;
        // Get what would be the next waypoint in the list
        Transform next_point = circuit.Waypoints[idx + 1];
        // Look at that point
        var look_pos = next_point.position - transform.position;
        transform.rotation = Quaternion.LookRotation(look_pos);
        // Start 10 meters in air
        transform.Translate(transform.up * 10);
        // Swap back in the non ragdoll
        SwapBack();
    }

    void Update() {
        if (gameObject.tag == "Player" && allow_jettison && Input.GetKeyUp(KeyCode.R)) {
            allow_jettison = false;
            Jettison();
            Invoke("AllowJettison", 3f);
        }
            /*
            if (Input.GetKeyUp(KeyCode.T)) {
                Jettison();
            }
            if (Input.GetKeyUp(KeyCode.Y)) {
                SwapBack();
            }*/
    }

    void Jettison() {
        // swap robot kyle with ragdoll
        normal_robot.SetActive(false);
        ragdoll_robot.SetActive(true);
        //ragdoll_robot.GetComponent<Rigidbody>().AddForce(transform.up * 20000, ForceMode.Impulse);
        m_Rigidbody.AddForce(transform.up * 10000, ForceMode.Impulse);
        Invoke("Respawn", 1f);
    }

    void SwapBack() {
        // swap ragdoll with robot kyle
        normal_robot.SetActive(true);
        Destroy(ragdoll_robot);
        ragdoll_robot = (GameObject)Instantiate(ragdoll_prefab, transform);
        //Parent it to the Car
        ragdoll_robot.transform.parent = gameObject.transform;
        ragdoll_robot.transform.localPosition = new Vector3(0f, 0.5f, 0.45f);
        ragdoll_robot.transform.localScale = new Vector3(2f, 2f, 2f);
    }

    void AllowJettison() {
        allow_jettison = true;
    }

    void Check_Pos() {
        Vector3 new_pos = transform.position;
        float distance = Vector3.Distance(new_pos, current_pos);
        if (distance < move_threshold) {
            //print("Distance less than 0.5 " + distance);
            Jettison();
        }
        current_pos = new_pos;
        Invoke("Check_Pos", time_to_wait);
    }

    Transform GetClosestWaypoint(Transform[] waypoints, out int idx)
    {
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        int counter=0, tIdx=0;
        foreach (Transform t in waypoints)
        {
            counter++;
            float dist = Vector3.Distance(t.position, currentPos);
            if (dist < minDist)
            {
                tMin = t;
                tIdx = counter;
                minDist = dist;
            }
        }
        idx=tIdx;
        return tMin;
    }

}
