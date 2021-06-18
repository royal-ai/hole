using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandInventory : MonoBehaviour {

    [Header ("Items")] //Items player has in his inventory (Weapon is WIP)
    public GameObject Slot1;
    public GameObject Slot2;
    public GameObject Slot3;
    string Slot1Current;
    string Slot2Current;
    string Slot3Current;

    [Header ("Stats")]
    public int selected;

	void Start () {
		
	}
	
	void Update ()
    {
        if(selected > 3)
        {
            selected = 0;
            Refresh();
        }
        if(selected < 0)
        {
            selected = 3;
            Refresh();
        }
	    if(Input.GetKeyDown("1"))
        {
            selected = 0;
            Refresh();
        }
        if (Input.GetKeyDown("2"))
        {
            selected = 1;
            Refresh();
        }
        if (Input.GetKeyDown("3"))
        {
            selected = 2;
            Refresh();
        }
        if(Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            selected++;
            Refresh();
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            selected--;
            Refresh();
        }

    }

    public void Refresh()
    {
        if(selected == 0)
        {
            Slot1.SetActive(true);
            Slot2.SetActive(false);
            Slot3.SetActive(false);
        }
        if (selected == 1)
        {
            Slot1.SetActive(false);
            Slot2.SetActive(true);
            Slot3.SetActive(false);
        }
        if (selected == 2)
        {
            Slot1.SetActive(false);
            Slot2.SetActive(false);
            Slot3.SetActive(true);
        }
    }
}
