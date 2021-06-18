using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mineable : MonoBehaviour {


    public float Health = 240;
    string ObjID;

    void Start()
    {
        ObjID = GetInstanceID().ToString() + "MINEABLE";
        Load();
    }
    public void Load()
    {
        if(PlayerPrefs.GetFloat(ObjID) != 0)
        {
            Health = PlayerPrefs.GetFloat(ObjID);
        }
        if(Health <= 0)
        {
            Destroy(gameObject);
        }

    }
    public void Save()
    {
        PlayerPrefs.SetFloat(ObjID, Health);
        PlayerPrefs.Save();
    }
    public void MineRefresh()
    {
        if(Health == 0)
        {
            Health = -1;
        }
        if (Health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
