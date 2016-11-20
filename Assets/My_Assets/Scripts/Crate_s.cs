using UnityEngine;
using System.Collections;

public class Crate_s : MonoBehaviour {

	Component collide;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider coll)
    {
		collide = GetComponent("BoxCollider");
		collide.GetType().GetProperty("enabled").SetValue(collide, false, null);
		collide = GetComponent("MeshRenderer");
		collide.GetType().GetProperty("enabled").SetValue(collide, false, null);
		respawnCrate(coll);
	}	
	
	IEnumerator respawnCrate(Collider col)
	{
		yield return new WaitForSeconds(30);
		Component collide = GetComponent("Box Collider");
		collide.GetType().GetProperty("enabled").SetValue(collide, true, null);
		collide = GetComponent("Mesh Renderer");
		collide.GetType().GetProperty("enabled").SetValue(collide, true, null);
	}
}
