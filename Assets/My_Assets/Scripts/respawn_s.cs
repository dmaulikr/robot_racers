using UnityEngine;
using System.Collections;
using UnityStandardAssets.Utility;

public class respawn_s : MonoBehaviour {

    private Rigidbody rb;
    //public WaypointList waypointList = new WaypointList();
    public WaypointCircuit circuit;

	// Use this for initialization
	void Awake () {
        rb = GetComponent<Rigidbody>();
	}

    void OnTriggerEnter(Collider col) {
        print("Collision!");
        if (col.gameObject.tag == "Kill") {
            print("Tag: Kill!");
            Respawn();
        }
    }

    void Respawn() {
        print("Respawn");
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
    }

    void Update() {
        if (Input.GetKeyUp(KeyCode.R)) {
            Respawn();
        }
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
