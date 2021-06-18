using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartPlayerPrefs : MonoBehaviour {

    public bool DebugMode = false; //If this is true you can press F8 in game to restart all stats and object locations

    public void Update() 
    {
        if(DebugMode)
        {
            if (Input.GetKeyDown("f8"))
            {
                Restart();
            }
        }

    }
    
    public void Restart()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("All playerprefs have been reset");
    }
}
