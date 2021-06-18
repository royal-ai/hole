using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilBarrel : MonoBehaviour {

    [Header ("Object stats")]
    public float fuel = 10; //Fuel it currenlty holds
    public float MaxFuel = 30f; //Maximum of fuel it can hold

   // [Header ("ID")]
    string ObjectID; //its own it, every object of this kind should have its own ID (Set these manually!)

	void Start ()
    {
        ObjectID = GetInstanceID().ToString() + "OILBARR";
        Load();	 
	}

    public void Load()
    {
        if(fuel > MaxFuel)
        {
            fuel = MaxFuel;
        }
        if(PlayerPrefs.GetFloat(ObjectID) != 0)
        {
            fuel = PlayerPrefs.GetFloat(ObjectID);
        }

    }

    public void Save()
    {
        if (fuel > MaxFuel)
        {
            fuel = MaxFuel;
        }
        PlayerPrefs.SetFloat(ObjectID, fuel);
        PlayerPrefs.Save();
    }
}
