using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eatable : MonoBehaviour {

    [Header ("Object Stats")]
    public float FoodAmount; //amount of food this object contains
    string FoodID; //ID of the food item (every new item has to get its own ID (this has to be done manually!))
    public float MaxFood = 4; //maximal amount of food this object can hold
    public float SaturationAmount = 250; //amount of food levels this object adds after consumption (players max is 1000)
    public float WaterAmount = 200; //amount of water levels this object adds after consumption (players max is 1000)

    [Header ("Gameobjects")]
    public GameObject fullCan; //FullCan model
    public GameObject emptyCan; //EmptyCan model

    SpawnObjectSaver objMan;


	void Start ()
    {
        FoodID = GetInstanceID().ToString() + "FOOD";
        Load();
        objMan = GetComponent<SpawnObjectSaver>();
	}
	


    public void Save() //Save object stats
    { 
        PlayerPrefs.SetFloat(FoodID, FoodAmount); 
        PlayerPrefs.Save();
        Load();

    }
    public void Load() //Load objects stats
    {

        if (PlayerPrefs.GetFloat(FoodID) != 0)
        {
            FoodAmount = PlayerPrefs.GetFloat(FoodID);
        }


        if (FoodAmount == 1) 
        {
            emptyCan.SetActive(true);
            fullCan.SetActive(false);
        }
        else
        {
            emptyCan.SetActive(false);
            fullCan.SetActive(true);
            objMan.alive = 1;
        }


    }

}
