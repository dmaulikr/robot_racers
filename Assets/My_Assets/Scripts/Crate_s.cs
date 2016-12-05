using UnityEngine;
using System.Collections;

public class Crate_s : MonoBehaviour {

    public BoxCollider my_box;
    public MeshRenderer my_mesh;
	
	void OnTriggerEnter(Collider coll)
    {
        my_box.enabled = false;
        my_mesh.enabled = false;
        Invoke("respawnCrate", 30f);
	}	
	
	void respawnCrate()
	{
        my_box.enabled = true;
        my_mesh.enabled = true;
	}
}
