using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilHolder : MonoBehaviour {

    [Header ("Stats")]
    public int HoldingNow = 0; //How many oil player currently holds (Can hold 2 max)

    [Header ("Mug models")]
    public GameObject MugFull;
    public GameObject MugHalf;
    public GameObject MugEmpty;

	void Start () {
        Load();
	}
	
    public void Save()
    {
        PlayerPrefs.SetInt("OilHold", HoldingNow);
        PlayerPrefs.Save();
        Load();
    }
    public void Load()
    {
        HoldingNow = PlayerPrefs.GetInt("OilHold");

        if(HoldingNow == 0)
        {
            MugEmpty.SetActive(true);
            MugHalf.SetActive(false);
            MugFull.SetActive(false);
        }
        if (HoldingNow == 1)
        {
            MugEmpty.SetActive(false);
            MugHalf.SetActive(true);
            MugFull.SetActive(false);
        }
        if (HoldingNow == 2)
        {
            MugEmpty.SetActive(false);
            MugHalf.SetActive(false);
            MugFull.SetActive(true);
        }
    }
    

}
