using UnityEngine;
using System.Collections;
using UnityStandardAssets.Vehicles.Car;

public class item_s : MonoBehaviour {

	public int heldItem;
	public invincible_s myscript;
	public float rand = 0;
	public GameObject oil;
	CarController myCarController;

    public game_controller_s my_game_controller;

	// Use this for initialization
	void Start () {
		heldItem = 0;
		myscript.invincible = false;
		Component halo = GetComponent("Halo"); 
		halo.GetType().GetProperty("enabled").SetValue(halo, false, null);
		myCarController = GetComponent<CarController> ();

        my_game_controller.oil_icon.SetActive(false);
        my_game_controller.lightning_icon.SetActive(false);

	}
	
	// Update is called once per frame
	void Update () {
		if (gameObject.tag == "Player" && Input.GetKeyUp(KeyCode.E))
			UseItem();
	}
	
	
	void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Crate")
        {
            GiveItem();
        }

		if (coll.gameObject.tag == "Oil") 
		{
			Vector3 difference = transform.position;
			difference = difference * -50f;

			print ("Collided with oil");

			myCarController.SpinOut(difference);

		}

		if (coll.gameObject.tag == "Invincible") 
		{
			//print ("col while invinc");
			invincible_s theirscript = coll.gameObject.GetComponent<invincible_s> ();

			if (theirscript.invincible == true) {
				//print ("get rigid");
				Vector3 difference = theirscript.myrigidbody.velocity;
				difference.Normalize();
				difference *= 15000f;
				myCarController.SpinOut(difference);
				print ("spin out");
			}
		}
	}

	void GiveItem()
	{
		float rand = Random.Range(0,2);

        if (rand >= 1) {
            heldItem = 2;
            if (gameObject.tag == "Player") {
                my_game_controller.lightning_icon.SetActive(false);
                my_game_controller.oil_icon.SetActive(true);
            } else if (gameObject.tag == "NPC") {
                Invoke("UseItem", 1f);
            }
        }

        if (rand < 1) {
            heldItem = 1;
            if (gameObject.tag == "Player") {
                my_game_controller.oil_icon.SetActive(false);
                my_game_controller.lightning_icon.SetActive(true);
            } else if (gameObject.tag == "NPC") {
                Invoke("UseItem", 1f);
            }
        }
	}
	
	void UseItem()
	{
		if(heldItem == 1)
		{
			Component halo = GetComponent("Halo"); 
			halo.GetType().GetProperty("enabled").SetValue(halo, true, null);
			
			myscript.invincible = true;
            myCarController.m_Topspeed = 75f;
			heldItem = 0;
            if (gameObject.tag == "Player") {
                print("Used lightning!");
                my_game_controller.oil_icon.SetActive(false);
                my_game_controller.lightning_icon.SetActive(false);
            }
			rand = 0;
			Invoke("EndInvincibility", 10f);
			
		}
		
		if(heldItem == 2)
		{
			Vector3 oilplace = transform.position - transform.forward * 3;
			Instantiate(oil, oilplace, Quaternion.identity);
		
			heldItem = 0;
            if (gameObject.tag == "Player") {
                print("Used Oil!");
                my_game_controller.lightning_icon.SetActive(false);
                my_game_controller.oil_icon.SetActive(false);
            }
			rand = 0;
		}
	}
	
	
	void EndInvincibility()
	{
		Component halo = GetComponent("Halo"); 
		halo.GetType().GetProperty("enabled").SetValue(halo, false, null);
		myscript.invincible = false;
        myCarController.m_Topspeed = 60f;
	}


}
