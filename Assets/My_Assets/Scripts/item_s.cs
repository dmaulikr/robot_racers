using UnityEngine;
using System.Collections;

public class item_s : MonoBehaviour {

	public int heldItem;
	public bool invincible;
	public float rand = 0;
	

	// Use this for initialization
	void Start () {
		heldItem = 0;
		invincible = false;
		Component halo = GetComponent("Halo"); 
		halo.GetType().GetProperty("enabled").SetValue(halo, false, null);
		
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	
	void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Crate")
        {
            GiveItem();
        }
	}
	
	
	void GiveItem()
	{
		float rand = Random.Range(1,2);
		if(rand < 1.5 && rand > 0)
			heldItem = 1;
			
		if(rand >= 1.5)
			heldItem = 2;
		
	}
	
	void UseItem()
	{
		if(heldItem == 1)
		{
			Component halo = GetComponent("Halo"); 
			halo.GetType().GetProperty("enabled").SetValue(halo, true, null);
			
			invincible = true;
			heldItem = 0;
			rand = 0;
			Invoke("EndInvincibility", 10f);
			
		}
		
		if(heldItem == 2)
		{
			//GameObject oil_slick = (GameObject)Instantiate(oil, transform.position, Quaternion.identity);
		
			heldItem = 0;
			rand = 0;
		}
	}
	
	
	void EndInvincibility()
	{
		Component halo = GetComponent("Halo"); 
		halo.GetType().GetProperty("enabled").SetValue(halo, false, null);
		invincible = false;
	}
}
