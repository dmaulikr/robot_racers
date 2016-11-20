using UnityEngine;
using System.Collections;

public class respawn_s : MonoBehaviour {

    private GameObject[] kill;
    public WaypointList waypointList;
    public Transform[] Waypoints {
        get { return waypointList.items; }
   }

	// Use this for initialization
	void Start () {
        kill = GameObject.FindGameObjectsWithTag("Kill");
	}

    void OnTriggerEnter(Collider col) {
        if (col.gameObject.tag == "Kill") {
            Transform respawn_point = GetClosestWaypoint(Waypoints);
            transform.position = respawn_point.position;
        }
    }

    Transform GetClosestWaypoint(Transform[] waypoints)
    {
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (Transform t in waypoints)
        {
            float dist = Vector3.Distance(t.position, currentPos);
            if (dist < minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }
        return tMin;
    }

}
