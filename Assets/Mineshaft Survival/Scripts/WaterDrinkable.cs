using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDrinkable : MonoBehaviour {

    [Header ("Object info")]
    public int WaterAmount = 8; //Amount of water it currently holds
    float refillValue; //Private value of this object (needed for auto refilling it self
    public bool refillAble = false; //if object should be automaticly refilled after time

    public string WaterID;

    [Header ("Heights")]
    public float l8; //Object height when 8/8 water inside
    public float l7; //Object height when 7/8 water inside
    public float l6; //Object height when 6/8 water inside
    public float l5; //Object height when 5/8 water inside
    public float l4; //Object height when 4/8 water inside
    public float l3; //Object height when 3/8 water inside
    public float l2; //Object height when 2/8 water inside
    public float l1; //Object height when 1/8 water inside

    public void Save()
    {
        PlayerPrefs.SetFloat(WaterID + "refill", refillValue);
        PlayerPrefs.SetInt(WaterID, WaterAmount);
        PlayerPrefs.Save();
        Load();
    }

    public void Load()
    {
        refillValue = PlayerPrefs.GetFloat(WaterID + "refill");


        if(PlayerPrefs.GetInt(WaterID) == 0)
        { }
        else
        {
            WaterAmount = PlayerPrefs.GetInt(WaterID);
        }

        if(WaterAmount == 8)
        {
            transform.position = new Vector3(transform.position.x, l8, transform.position.z);
        }
        if (WaterAmount == 7)
        {
            transform.position = new Vector3(transform.position.x, l7, transform.position.z);
        }
        if (WaterAmount == 6)
        {
            transform.position = new Vector3(transform.position.x, l6, transform.position.z);
        }
        if (WaterAmount == 5)
        {
            transform.position = new Vector3(transform.position.x, l5, transform.position.z);
        }
        if (WaterAmount == 4)
        {
            transform.position = new Vector3(transform.position.x, l4, transform.position.z);
        }
        if (WaterAmount == 3)
        {
            transform.position = new Vector3(transform.position.x, l3, transform.position.z);
        }
        if (WaterAmount == 2)
        {
            transform.position = new Vector3(transform.position.x, l2, transform.position.z);
        }
        if (WaterAmount == 1)
        {
            transform.position = new Vector3(transform.position.x, l1, transform.position.z);
        }

    }

    void Update()
    {
        if(refillValue >= 100) //if refill value is 100 add one to waterAmount
        {
            if(WaterAmount == 8)
            { }
            else
            {
                WaterAmount++;
                refillValue = 0;
            }
        }
    }


	void Start ()
    {
        Load();
        StartCoroutine(refill());
    }
    IEnumerator refill()
    {
        if(refillAble == true)
        {
            yield return new WaitForSeconds(9); //Add one to refillValue (when refillValue is 100 it adds one to WaterAmount that player can drink)
            if (WaterAmount != 8)
            {
                refillValue++;
                Save();
            }
            StartCoroutine(refill());
        }


    }
 

}
