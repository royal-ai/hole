using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lantern : MonoBehaviour {

    [Header ("Lamp Stats")]
    public float Fuel = 1000; //Fuel lamp currently has
    public bool toggled = true; //if lamp is on or off

    [Header ("Materials")]
    public Material LampOn; //lamp on material
    public Material LampOff; //lamp off material

    [Header ("Gameobjects")]
    public GameObject Light; //light gameobject
    public Renderer Glass; //the object that has to change its color depending on toggled value
    public GameObject Player; //player gameobject

    [Header ("ID")]
    string LampID; //ID that has to be set for every gameobject of this type (has to be done manually)
    string LampToggleID;


   

    public void LoadSettings()
    {
        if (PlayerPrefs.GetFloat(LampID) == 0)
        {

        }
        else
        {
            Fuel = PlayerPrefs.GetFloat(LampID);
        }



        if (PlayerPrefs.GetInt(LampToggleID) == 1)
        {
            toggled = true;
        }
        if (PlayerPrefs.GetInt(LampToggleID) == 2)
        {
            toggled = false;
        }

        if(toggled == true)
        {
            TurnOn();
        }
        else
        {
            TurnOff();
        }


    }
    public void SaveSettings() //Save lamps settings
    { 
        if(Fuel > 1000)
        {
            Fuel = 1000;
        }

        PlayerPrefs.SetFloat(LampID, Fuel);

        if(toggled == true)
        {
              PlayerPrefs.SetInt(LampToggleID, 1);
        }
        if (toggled == false)
        {
            PlayerPrefs.SetInt(LampToggleID, 2);
        }
        PlayerPrefs.Save();
        Debug.Log("Saved Lamp settings " + LampID + " " + PlayerPrefs.GetInt(LampToggleID));
    }


    IEnumerator AutoSave() //auto saving lamps settings every 6 seconds
    {
        yield return new WaitForSeconds(6);
        SaveSettings();
        StartCoroutine(AutoSave());
    }













    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        LampID = GetInstanceID().ToString() + "LAMP";
        LampToggleID = LampID + "togg";
        LoadSettings();
        StartCoroutine(AutoSave());
    }

	void Update ()
    {
		if(toggled == true)
        {
            float distance = Vector3.Distance(gameObject.transform.position, Player.transform.position);
            if(distance >= 50)
            {
                Fuel -= 0.005f;
            }
            else
            {

            }
            Fuel -= 0.02f;
            if (Fuel <= 1)
            {
                TurnOff();
            }
        }
        
	}
    public void TurnOn()
    {
        toggled = true;
        Glass.material = LampOn; //if lamp gets turned on change its glass material to LampOn
        Light.SetActive(true);
    }
    public void TurnOff()
    {
        toggled = false;
        Glass.material = LampOff;//if lamp gets turned off change its glass material to LampOff
        Light.SetActive(false);
    }
    public void Toggle()
    {
        toggled = !toggled; //toggle lamp
        if(toggled == true)
        {
            TurnOn();
        }
        if (toggled == false)
        {
            TurnOff();
        }
        SaveSettings(); //Save lamps settings
    }
}
