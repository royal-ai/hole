using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantManager : MonoBehaviour {

    public string[] LoadedPlants;
    public string AllLoaded;

    public GameObject pickle;

	void Start ()
    {
        AllLoaded = PlayerPrefs.GetString("PLANTS");
        LoadedPlants = AllLoaded.Split(new string[] { "," }, StringSplitOptions.None);


        foreach (string s in LoadedPlants)
        {
            if(s.Contains("Pickle"))
            {

                if (PlayerPrefs.GetFloat(s + "g") == 4)
                {

                }
                else
                {
                    Vector3 SLoc = new Vector3(PlayerPrefs.GetFloat(s + "x"), PlayerPrefs.GetFloat(s + "y"), PlayerPrefs.GetFloat(s + "z"));
                    GameObject PicklePlant = Instantiate(pickle, SLoc, transform.rotation);
                    PicklePlant.GetComponent<Plant>().PlantNumber = s;
                    PicklePlant.transform.position = SLoc;
                }
                

            }
        }
    }
}
