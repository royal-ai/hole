using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour {

    public string[] LoadedObjects;
    public string AllLoaded;

    public GameObject PSEED;
    public GameObject PEAT;

    void Start()
    {
        AllLoaded = PlayerPrefs.GetString("OBJECT");
        LoadedObjects = AllLoaded.Split(new string[] { "," }, StringSplitOptions.None);


        foreach (string s in LoadedObjects)
        {
            if (s.Contains("PSEED"))
            {

                if (PlayerPrefs.GetFloat(s + "a") == 1)
                {

                }
                else
                {
                    Vector3 SLoc = new Vector3(PlayerPrefs.GetFloat(s + "px"), PlayerPrefs.GetFloat(s + "py"), PlayerPrefs.GetFloat(s + "pz"));
                    GameObject PickleSeed = Instantiate(PSEED, SLoc, transform.rotation);
                    PickleSeed.GetComponent<SpawnObjectSaver>().ObjNumber = s;
                    PickleSeed.transform.position = SLoc;
                }


            }

            if (s.Contains("PEAT"))
            {

                if (PlayerPrefs.GetFloat(s + "a") == 1)
                {

                }
                else
                {
                    Vector3 SLoc = new Vector3(PlayerPrefs.GetFloat(s + "px"), PlayerPrefs.GetFloat(s + "py"), PlayerPrefs.GetFloat(s + "pz"));
                    GameObject PickleEatable = Instantiate(PEAT, SLoc, transform.rotation);
                    PickleEatable.GetComponent<SpawnObjectSaver>().ObjNumber = s;
                    PickleEatable.transform.position = SLoc;
                }


            }
        }
    }
}
