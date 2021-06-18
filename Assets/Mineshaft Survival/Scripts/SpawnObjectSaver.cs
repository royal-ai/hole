using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjectSaver : MonoBehaviour
{
    public int alive = 0;
    public string ObjType;
    public string ObjNumber = "0";

    string ObjID;

    void Start()
    {
        if (ObjNumber == "0")
        {
            ObjNumber = Random.Range(0, 99999).ToString();
        }
        string CurrentPref = PlayerPrefs.GetString("OBJECT");
        ObjID = ObjNumber + ObjType;
        if (!CurrentPref.Contains(ObjID))
        {
            if (!ObjNumber.Contains(ObjType))
            {
                PlayerPrefs.SetString("OBJECT", PlayerPrefs.GetString("OBJECT") + ObjID + ",");
            }

        }
        if (alive == 1)
        {
            Destroy(gameObject);
        }
        Save();
    }

    public void Save()
    {
        PlayerPrefs.SetFloat(ObjID + "px", transform.position.x);
        PlayerPrefs.SetFloat(ObjID + "py", transform.position.y);
        PlayerPrefs.SetFloat(ObjID + "pz", transform.position.z);
        PlayerPrefs.SetInt(ObjID + "a", alive);

        PlayerPrefs.Save();
    
    }

}
